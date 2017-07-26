namespace DataAccessLayer
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Data.Common;
	using DataAccessLayer.Models;

    public class OrdersRepository
    {
		private const int DefaultIntValue = -1;

		private DbProviderFactory factory;

		private string connectionString;

		public OrdersRepository()
		{
			var connectionStringItem = ConfigurationManager.ConnectionStrings["NorthwindConection"];
			connectionString = connectionStringItem.ConnectionString;
			var providerName = connectionStringItem.ProviderName;
			factory = DbProviderFactories.GetFactory(providerName);
		}

		public List<Order> GetAllOrders()
		{
			var orders = new List<Order>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT OrderID, CustomerID, EmployeeID, OrderDate, ShippedDate " +
					"FROM Northwind.Orders " +
					"ORDER BY OrderID";

				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var order = new Order();
						order.OrderID = (int)reader["OrderID"];//todo pn для перестраховки можно обработку исключения invalidCastExeption добавить (мы в базе можем с int на smallint заменить например)
						order.CustomerID = (string)reader["CustomerID"];
						order.EmployeeID = (int)reader["EmployeeID"];

						if (reader.IsDBNull(reader.GetOrdinal("OrderDate")))
						{
							order.Status = OrderStatus.New;
						}
						else if (reader.IsDBNull(reader.GetOrdinal("ShippedDate")))
						{
							order.OrderDate = (DateTime)reader["OrderDate"];
							order.Status = OrderStatus.InProcess;
						}
						else
						{
							order.OrderDate = (DateTime)reader["OrderDate"];
							order.ShippedDate = (DateTime)reader["ShippedDate"];
							order.Status = OrderStatus.Done;
						}

						orders.Add(order);
					}
				}
			}

			return orders;
		}

		public List<OrderDetail> GetOrderDetails(int orderId)
		{
			var details = new List<OrderDetail>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT o.OrderID, p.ProductID, p.ProductName " +
					"FROM Northwind.Orders AS o " +
					"JOIN Northwind.[Order Details] AS od " +
					"ON o.OrderID = od.OrderID " +
					"JOIN Northwind.Products AS p " +
					"ON od.ProductID = p.ProductID " +
					"WHERE od.OrderID = @orderId " +
					"ORDER BY p.ProductID;";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@orderId";//todo pn .AddWithValue не понравился?
				orderIdParam.DbType = DbType.Int32;
				orderIdParam.Value = orderId;
				command.Parameters.Add(orderIdParam);

				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var detail = new OrderDetail();
						detail.OrderID = (int)reader["OrderID"];
						detail.ProductID = (int)reader["ProductID"];
						detail.ProductName = (string)reader["ProductName"];
						details.Add(detail);
					}
				}
			}

			return details;
		}

		public int CreateOrder(CreatingOrder order)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"INSERT INTO Northwind.Orders (CustomerID, EmployeeID" +
					", ShipName, ShipAddress, ShipCity" +
					", ShipRegion, ShipPostalCode, ShipCountry) " +
					"SELECT @custId, @empId " +
					", c.CompanyName, c.Address, c.City" +
					", c.Region, c.PostalCode, c.Country " +
					"FROM Northwind.Customers AS c " +
					"WHERE c.CustomerID = @custId; " +
					"SELECT CONVERT(int, SCOPE_IDENTITY()); ";

				var custId = command.CreateParameter();
				custId.ParameterName = "@custId";
				custId.DbType = DbType.String;
				custId.Value = order.CustomerID;

				var empId = command.CreateParameter();
				empId.ParameterName = "@empId";
				empId.DbType = DbType.Int32;
				empId.Value = order.EmployeeID;

				command.Parameters.AddRange(new[] { custId, empId });
				connection.Open();

				try
				{
					return (int)command.ExecuteScalar();
				}
				catch//todo pn не нужно перехватывать все исключительные ситуации. лучше перехватить SQLException
				{
					return DefaultIntValue;//todo pn немного странная логика, если у нас что-то пошло не так и запись не создалась, для чего нам возвращать ИД? Лучше пусть выбросится исключение, которое мы обработаем не здесь, а на уровень выше.
				}
			}
		}

		public void SetOrderDate(int orderId, DateTime date)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"UPDATE Northwind.Orders " +
					"SET OrderDate = @date " +
					"WHERE OrderID = @orderId ";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@orderId";
				orderIdParam.DbType = DbType.Int32;
				orderIdParam.Value = orderId;
				command.Parameters.Add(orderIdParam);

				var dateParam = command.CreateParameter();
				dateParam.ParameterName = "@date";
				dateParam.DbType = DbType.DateTime;
				dateParam.Value = date;
				command.Parameters.Add(dateParam);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public void SetShippedDate(int orderId, DateTime date)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"UPDATE Northwind.Orders " +
					"SET ShippedDate = @date " +
					"WHERE OrderID = @orderId ";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@orderId";
				orderIdParam.DbType = DbType.Int32;
				orderIdParam.Value = orderId;
				command.Parameters.Add(orderIdParam);

				var dateParam = command.CreateParameter();
				dateParam.ParameterName = "@date";
				dateParam.DbType = DbType.DateTime;
				dateParam.Value = date;
				command.Parameters.Add(dateParam);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public List<CustomerOrderHistory> GetCustomerOrderHistory(string customerId)
		{
			var history = new List<CustomerOrderHistory>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "Northwind.CustOrderHist";

				var custIdParam = command.CreateParameter();
				custIdParam.ParameterName = "@CustomerID";
				custIdParam.DbType = DbType.String;
				custIdParam.Value = customerId;
				command.Parameters.Add(custIdParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var order = new CustomerOrderHistory();
						order.ProductName = (string)reader["ProductName"];
						order.Total = (int)reader["Total"];
						history.Add(order);
					}
				}
			}

			return history;
		}

		public List<CustomerOrderDetail> GetCustomerOrderDetails(int orderId)
		{
			var details = new List<CustomerOrderDetail>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandType = CommandType.StoredProcedure;
				command.CommandText = "Northwind.CustOrdersDetail";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@OrderID";
				orderIdParam.DbType = DbType.Int32;
				orderIdParam.Value = orderId;
				command.Parameters.Add(orderIdParam);

				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						//var detail = new CustomerOrderDetail();
						//detail.ProductName = (string)reader["ProductName"];
						//detail.UnitPrice = (decimal)reader["UnitPrice"];
						//detail.Quantity = (short)reader["Quantity"];
						//detail.Discount = (int)reader["Discount"];
						//detail.ExtendedPrice = (decimal)reader["ExtendedPrice"];
						//details.Add(detail);
						details.Add(new CustomerOrderDetail //todo pn так экономнее писать
						{
							ProductName = (string)reader["ProductName"],
							UnitPrice = (decimal)reader["UnitPrice"],
							Quantity = (short)reader["Quantity"],
							Discount = (int)reader["Discount"],
							ExtendedPrice = (decimal)reader["ExtendedPrice"]
					});
					}
				}
			}

			return details;
		}
	}
}
