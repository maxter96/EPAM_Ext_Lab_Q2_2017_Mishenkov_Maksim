namespace DataAccessLayer
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Data.Common;
	using DataAccessLayer.Models;
	using System.Linq;

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

		public List<Order> GetAllOrders(bool withSum)
		{
			var orders = new List<Order>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT o.OrderID, o.CustomerID, o.EmployeeID, o.OrderDate, o.ShippedDate, " +
					"o.RequiredDate, o.ShipCountry, o.ShipCity, o.ShipAddress, o.ShipName, " +
					"(SELECT c.CompanyName FROM Northwind.Customers AS c " +
					"WHERE c.CustomerID = o.CustomerID) AS CompanyName, " +
					"(SELECT CONCAT(e.FirstName, ' ', e.LastName) FROM Northwind.Employees AS e " +
					"WHERE e.EmployeeID = o.EmployeeID) AS EmployeeName " +
					"FROM Northwind.Orders AS o " +
					"ORDER BY o.OrderID";

				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var order = new Order();
						order.OrderID = (int)reader["OrderID"];
						order.CustomerID = reader["CustomerID"].ToString();
						order.EmployeeID = (int)reader["EmployeeID"];
						order.CompanyName = reader["CompanyName"].ToString();
						order.ShipAddress = reader["ShipAddress"].ToString();
						order.ShipCity = reader["ShipCity"].ToString();
						order.ShipCountry = reader["ShipCountry"].ToString();
						order.ShipName = reader["ShipName"].ToString();
						order.EmployeeName = reader["EmployeeName"].ToString();

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

						if (!reader.IsDBNull(reader.GetOrdinal("RequiredDate")))
						{
							order.RequiredDate = (DateTime)reader["RequiredDate"];
						}

						orders.Add(order);
					}
				}
			}

			if (withSum)
			{
				foreach (var order in orders)
				{
					order.Sum = GetOrderDetails(order.OrderID).Sum(x => x.Sum);
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
					"SELECT o.OrderID, p.ProductID, p.ProductName, " +
					"(od.UnitPrice - od.UnitPrice * od.Discount) * od.Quantity AS Sum " +
					"FROM Northwind.Orders AS o " +
					"JOIN Northwind.[Order Details] AS od " +
					"ON o.OrderID = od.OrderID " +
					"JOIN Northwind.Products AS p " +
					"ON od.ProductID = p.ProductID " +
					"WHERE od.OrderID = @orderId " +
					"ORDER BY p.ProductID;";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@orderId";
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
						detail.ProductName = reader["ProductName"].ToString();
						detail.Sum = (float)reader["Sum"];
						details.Add(detail);
					}
				}
			}

			return details;
		}

		public void CreateOrder(CreatingOrder order)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"INSERT INTO Northwind.Orders (CustomerID, EmployeeID" +
					", OrderDate, ShippedDate, RequiredDate" +
					", ShipName, ShipAddress, ShipCity" +
					", ShipRegion, ShipPostalCode, ShipCountry) " +
					"SELECT @custId, @empId " +
					", @ordDate, @shipDate, @reqDate" +
					", c.CompanyName, c.Address, c.City" +
					", c.Region, c.PostalCode, c.Country " +
					"FROM Northwind.Customers AS c " +
					"WHERE c.CustomerID = @custId; ";

				var custId = command.CreateParameter();
				custId.ParameterName = "@custId";
				custId.DbType = DbType.String;
				custId.Value = order.CustomerID;

				var empId = command.CreateParameter();
				empId.ParameterName = "@empId";
				empId.DbType = DbType.Int32;
				empId.Value = order.EmployeeID;

				var ordDate = command.CreateParameter();
				ordDate.ParameterName = "@ordDate";
				ordDate.DbType = DbType.DateTime;
				ordDate.Value = order.OrderDate;

				var shipDate = command.CreateParameter();
				shipDate.ParameterName = "@shipDate";
				shipDate.DbType = DbType.DateTime;
				shipDate.Value = order.ShippedDate;

				var reqDate = command.CreateParameter();
				reqDate.ParameterName = "@reqDate";
				reqDate.DbType = DbType.DateTime;
				reqDate.Value = order.RequiredDate;

				command.Parameters.AddRange(new[] { custId, empId, ordDate, shipDate, reqDate });
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public void UpdateOrder(CreatingOrder order)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"UPDATE Northwind.Orders " +
					"SET CustomerID = @custId, EmployeeID = @empId, " +
					"OrderDate = @ordDate, ShippedDate = @shipDate, RequiredDate = @reqDate " +
					"WHERE OrderID = @orderId ";

				var custId = command.CreateParameter();
				custId.ParameterName = "@custId";
				custId.DbType = DbType.String;
				custId.Value = order.CustomerID;

				var empId = command.CreateParameter();
				empId.ParameterName = "@empId";
				empId.DbType = DbType.Int32;
				empId.Value = order.EmployeeID;

				var orderId = command.CreateParameter();
				orderId.ParameterName = "@orderId";
				orderId.DbType = DbType.Int32;
				orderId.Value = order.OrderID;

				var ordDate = command.CreateParameter();
				ordDate.ParameterName = "@ordDate";
				ordDate.DbType = DbType.DateTime;
				ordDate.Value = order.OrderDate;

				var shipDate = command.CreateParameter();
				shipDate.ParameterName = "@shipDate";
				shipDate.DbType = DbType.DateTime;
				shipDate.Value = order.ShippedDate;

				var reqDate = command.CreateParameter();
				reqDate.ParameterName = "@reqDate";
				reqDate.DbType = DbType.DateTime;
				reqDate.Value = order.RequiredDate;

				command.Parameters.AddRange(new[] { orderId, custId, empId, ordDate, shipDate, reqDate });
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public void DeleteOrder(int orderId)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"DELETE FROM Northwind.[Order Details] " +
					"WHERE OrderID = @orderId;" +
					"DELETE FROM Northwind.Orders " +
					"WHERE OrderID = @orderId;";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@orderId";
				orderIdParam.DbType = DbType.Int32;
				orderIdParam.Value = orderId;
				command.Parameters.Add(orderIdParam);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public List<Product> GetAllProducts()
		{
			var products = new List<Product>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
                command.CommandText =
					"SELECT ProductID, ProductName " +
					"FROM Northwind.Products;";
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var product = new Product();
						product.ProductID = (int)reader["ProductID"];
						product.ProductName = reader["ProductName"].ToString();
						products.Add(product);
					}
				}

				return products;
			}
		}

		public void AddOrderDetail(CreatingOrderDetail detail)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"IF EXISTS " +
					"(SELECT ProductID FROM Northwind.[Order Details] " +
					"WHERE OrderID = @orderId AND ProductID = @productId) " +
					"UPDATE Northwind.[Order Details] " +
					"SET Quantity = Quantity + @quantity, Discount = @discount " +
					"WHERE OrderID = @orderId AND ProductID = @productId " +
					"ELSE " +
					"INSERT INTO Northwind.[Order Details] " +
					"(OrderID, ProductID, Quantity, Discount, UnitPrice) " +
					"VALUES(@orderId, @productId, @quantity, @discount, " +
					"(SELECT p.UnitPrice " +
					"FROM Northwind.Products AS p " +
					"WHERE p.ProductID = @productId));";

				var orderIdParam = command.CreateParameter();
				orderIdParam.ParameterName = "@orderId";
				orderIdParam.DbType = DbType.Int32;
				orderIdParam.Value = detail.OrderID;
				command.Parameters.Add(orderIdParam);

				var productIdParam = command.CreateParameter();
				productIdParam.ParameterName = "@productId";
				productIdParam.DbType = DbType.Int32;
				productIdParam.Value = detail.ProductID;
				command.Parameters.Add(productIdParam);

				var quantityParam = command.CreateParameter();
				quantityParam.ParameterName = "@quantity";
				quantityParam.DbType = DbType.Int32;
				quantityParam.Value = detail.Quantity;
				command.Parameters.Add(quantityParam);

				var discountParam = command.CreateParameter();
				discountParam.ParameterName = "@discount";
				discountParam.DbType = DbType.Single;
				discountParam.Value = detail.Discount;
				command.Parameters.Add(discountParam);

				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public List<Customer> GetAllCustomers()
		{
			var customers = new List<Customer>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT CustomerID, CompanyName " +
					"FROM Northwind.Customers;";
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var customer = new Customer();
						customer.CompanyName = reader["CompanyName"].ToString();
						customer.CustomerID = reader["CustomerID"].ToString();
						customers.Add(customer);
					}
				}

				return customers;
			}
		}

		public List<Employee> GetAllEmployees()
		{
			var employees = new List<Employee>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT EmployeeID, " +
					"CONCAT(FirstName, ' ', LastName) AS Name " +
					"FROM Northwind.Employees";
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var employee = new Employee();
						employee.EmployeeID = (int)reader["EmployeeID"];
						employee.Name = reader["Name"].ToString();
						employees.Add(employee);
					}
				}

				return employees;
			}
		}
	}
}
