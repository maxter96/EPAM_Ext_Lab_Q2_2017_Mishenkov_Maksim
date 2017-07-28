namespace DataAccessLayerTest
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data;
	using System.Data.Common;
	using System.Linq;
	using DataAccessLayer;
	using DataAccessLayer.Models;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class DataAccessLayerTest
	{
		private string connectionString;
		private string providerName;
		private DbProviderFactory factory;
		private OrdersRepository repo;

		public DataAccessLayerTest()
		{
			connectionString = ConfigurationManager.ConnectionStrings["NorthwindConection"].ConnectionString;
			providerName = ConfigurationManager.ConnectionStrings["NorthwindConection"].ProviderName;
			factory = DbProviderFactories.GetFactory(providerName);
			repo = new OrdersRepository();
		}

		[TestMethod]
		public void OrdersCount()
		{
			int count = 0;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "SELECT COUNT(*) FROM Northwind.Orders";
				connection.Open();
				count = (int)command.ExecuteScalar();
			}

			var items = repo.GetAllOrders();
			Assert.AreEqual(count, items.Count, "Получены не все элементы!");
		}

		[TestMethod]
		public void ProductsInOrderDetails()
		{
			List<int> products = new List<int>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT p.ProductID " +
					"FROM Northwind.Products AS p " +
					"JOIN Northwind. [Order Details] AS od " +
					"ON p.ProductID = od.ProductID " +
					"WHERE od.OrderID = 10285 " +//todo pn хардкод (мало ли у тебя тестовая база изменится и придется шерстить все тесты на наличие подобных вставок)
					"ORDER BY p.ProductID;";
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						products.Add((int)reader["ProductID"]);
					}
				}
			}

			var details = repo.GetOrderDetails(10285);
			var result = products.SequenceEqual(details.Select(x => x.ProductID));
			Assert.AreEqual(result, true, "Получены неверные данные!");
		}

		[TestMethod]
		public void ProductNamesInOrderHistory()
		{
			List<string> productNames = new List<string>();
			var customerId = "QUICK";

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT p.ProductName " +
					"FROM Northwind.Customers AS c " +
					"JOIN Northwind.Orders AS o " +
					"ON o.CustomerID = c.CustomerID " +
					"JOIN Northwind. [Order Details] AS od " +
					"ON o.OrderID = od.OrderID " +
					"JOIN Northwind.Products AS p " +
					"ON od.ProductID = p.ProductID " +
					"WHERE c.CustomerID = @customerId " +
					"GROUP BY p.ProductName " +
					"ORDER BY p.ProductName;";

				var custIdParam = command.CreateParameter();
				custIdParam.ParameterName = "@customerId";
				custIdParam.DbType = DbType.String;
				custIdParam.Value = customerId;
				command.Parameters.Add(custIdParam);

				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						productNames.Add((string)reader["ProductName"]);
					}
				}
			}

			var history = repo.GetCustomerOrderHistory(customerId);
			var result = productNames.SequenceEqual(history.Select(x => x.ProductName));
			Assert.AreEqual(result, true);
		}

		[TestMethod]
		public void ProductsQuantityInOrderDetails()
		{
			int sumQuantity = 0;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT SUM(od.Quantity) " +
					"FROM Northwind.Orders AS o " +
					"JOIN Northwind.[Order Details] AS od " +
					"ON o.OrderID = od.OrderID " +
					"WHERE o.OrderID = 10285; ";//todo pn аналогично

				connection.Open();
				sumQuantity = (int)command.ExecuteScalar();
			}

			var details = repo.GetCustomerOrderDetails(10285);
			Assert.AreEqual(details.Sum(x => x.Quantity), sumQuantity);
		}

		[TestMethod]
		public void OrderCreating()
		{
			var order = new CreatingOrder()
			{
				CustomerID = "QUICK",
				EmployeeID = 1,
			};

			int orderId = repo.CreateOrder(order);
			var orders = new List<CreatingOrder>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"SELECT o.CustomerID, o.EmployeeID " +
					"FROM Northwind.Orders AS o " +
					"WHERE o.OrderID = " + orderId;

				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						orders.Add(new CreatingOrder()
						{
							CustomerID = (string)reader["CustomerID"],
							EmployeeID = (int)reader["EmployeeID"]
						});
					}
				}

				command.CommandText =
					"DELETE FROM Northwind.Orders " +
					"WHERE OrderID = " + orderId;

				command.ExecuteNonQuery();
			}

			Assert.AreEqual(orders.Count, 1, "Неверное количество созданных элементов!");
			Assert.AreEqual(orders[0].CustomerID, order.CustomerID, "Получен неверный CustomerID");
			Assert.AreEqual(orders[0].EmployeeID, order.EmployeeID, "Получен неверный EmployeeID");
		}

		[TestMethod]
		public void SettingOrderDate()
		{
			var customerId = "QUICK";
			var employeeId = 1;
			int orderId = 0;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"INSERT INTO Northwind.Orders (CustomerID, EmployeeID)" +
					"VALUES" + string.Format("('{0}', {1})", customerId, employeeId) +
					"SELECT CONVERT(int, SCOPE_IDENTITY()); ";
				connection.Open();
				orderId = (int)command.ExecuteScalar();
			}

			repo.SetOrderDate(orderId, DateTime.Now);
			var newOrder = repo.GetAllOrders()
				.Where(x => x.OrderID == orderId)
				.First();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "DELETE FROM Northwind.Orders WHERE OrderID = " + orderId;
				connection.Open();
				command.ExecuteNonQuery();
			}

			Assert.AreEqual(newOrder.Status, OrderStatus.InProcess, "Заказ должен быть в работе!");
		}

		[TestMethod]
		public void SettingShippedDate()
		{
			var customerId = "QUICK";
			var employeeId = 1;
			int orderId = 0;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText =
					"INSERT INTO Northwind.Orders (CustomerID, EmployeeID, OrderDate)" +
					string.Format("VALUES('{0}', {1}, @date)", customerId, employeeId) +
					"SELECT CONVERT(int, SCOPE_IDENTITY()); ";

                var date = command.CreateParameter();
                date.DbType = DbType.Date;
                date.ParameterName = "@date";
                date.Value = DateTime.Now;
                command.Parameters.Add(date);

				connection.Open();
				orderId = (int)command.ExecuteScalar();
			}

			repo.SetShippedDate(orderId, DateTime.Now);
			var newOrder = repo.GetAllOrders()
				.Where(x => x.OrderID == orderId)
				.First();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "DELETE FROM Northwind.Orders WHERE OrderID = " + orderId;
				connection.Open();
				command.ExecuteNonQuery();
			}

			Assert.AreEqual(newOrder.Status, OrderStatus.Done, "Заказ должен быть выполненым!");
		}
	}
}
