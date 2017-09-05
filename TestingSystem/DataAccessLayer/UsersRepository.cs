using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using DataAccessLayer.Models;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace DataAccessLayer
{
	public class UsersRepository
	{
		public const int DefaultErrorCode = -1;

		public const int MaxLoginLength = 20;

		public const int MaxPasswordLength = 20;

		public const int MaxFirstNameLength = 20;

		public const int MaxLastNameLength = 20;

		public const int MaxRoleNameLength = 10;

		private DbProviderFactory factory;

		private string connectionString;

		public UsersRepository()
		{
			var connectionStringItem = ConfigurationManager.ConnectionStrings["TestingSystemDBConnection"];
			connectionString = connectionStringItem.ConnectionString;
			var providerName = connectionStringItem.ProviderName;
			factory = DbProviderFactories.GetFactory(providerName);
		}

		public int AddUserToRole(string userName, string roleName)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[AddUserToRole]";
				command.CommandType = CommandType.StoredProcedure;

				var userNameParam = command.CreateParameter();
				userNameParam.ParameterName = "@Login";
				userNameParam.DbType = DbType.StringFixedLength;
				userNameParam.Value = userName;

				var roleNameParam = command.CreateParameter();
				roleNameParam.ParameterName = "@RoleName";
				roleNameParam.DbType = DbType.StringFixedLength;
				roleNameParam.Value = roleName;

				command.Parameters.AddRange(new[] { userNameParam, roleNameParam });
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public int CreateRole(string roleName)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[CreateRole]";
				command.CommandType = CommandType.StoredProcedure;

				var roleNameParam = command.CreateParameter();
				roleNameParam.ParameterName = "@RoleName";
				roleNameParam.DbType = DbType.StringFixedLength;
				roleNameParam.Value = roleName;

				command.Parameters.Add(roleNameParam);
				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public void DeleteRole(string roleName)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[DeleteRole]";
				command.CommandType = CommandType.StoredProcedure;

				var roleNameParam = command.CreateParameter();
				roleNameParam.ParameterName = "@RoleName";
				roleNameParam.DbType = DbType.StringFixedLength;
				roleNameParam.Value = roleName;

				command.Parameters.Add(roleNameParam);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public Guid GetHashString(string password)
		{ 
			byte[] bytes = Encoding.Unicode.GetBytes(password);
			MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
			byte[] byteHash = CSP.ComputeHash(bytes);
			StringBuilder hash = new StringBuilder();

			foreach (byte b in byteHash)
			{
				hash.AppendFormat("{0:x2}", b);
			}

			return new Guid(hash.ToString());
		}

		public int CreateUser(CreatingUserModel user)
		{
			var result = DefaultErrorCode;

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[CreateUser]";
				command.CommandType = CommandType.StoredProcedure;

				var loginParam = command.CreateParameter();
				loginParam.ParameterName = "@Login";
				loginParam.DbType = DbType.StringFixedLength;
				loginParam.Value = user.Login;

				var passwordParam = command.CreateParameter();
				passwordParam.ParameterName = "@Password";
				passwordParam.DbType = DbType.Guid;
				passwordParam.Value = GetHashString(user.Password);

				var firstNameParam = command.CreateParameter();
				firstNameParam.ParameterName = "@FirstName";
				firstNameParam.DbType = DbType.StringFixedLength;
				firstNameParam.Value = user.FirstName;

				var lastNameParam = command.CreateParameter();
				lastNameParam.ParameterName = "@LastName";
				lastNameParam.DbType = DbType.StringFixedLength;
				lastNameParam.Value = user.LastName;

				command.Parameters.AddRange(new[] 
				{
					loginParam,
					passwordParam,
					firstNameParam,
					lastNameParam
				});

				connection.Open();
				result = (int)command.ExecuteScalar();
			}

			return result;
		}

		public void DeleteUser(int userId)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[DeleteUser]";
				command.CommandType = CommandType.StoredProcedure;

				var userIdParam = command.CreateParameter();
				userIdParam.ParameterName = "@UserID";
				userIdParam.DbType = DbType.Int32;
				userIdParam.Value = userId;

				command.Parameters.Add(userIdParam);
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public string[] GetAllRoles()
		{
			var roles = new List<string>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[GetAllRoles]";
				command.CommandType = CommandType.StoredProcedure;
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var role = (string)reader["RoleName"];
						roles.Add(role);
					}
				}
			}

			return roles.ToArray();
		}

		public string[] GetRolesForUser(string userName)
		{
			var roles = new List<string>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[GetRolesForUser]";
				command.CommandType = CommandType.StoredProcedure;

				var userNameParam = command.CreateParameter();
				userNameParam.ParameterName = "@Login";
				userNameParam.DbType = DbType.StringFixedLength;
				userNameParam.Value = userName;

				command.Parameters.Add(userNameParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var role = (string)reader["RoleName"];
						roles.Add(role);
					}
				}
			}

			return roles.ToArray();
		}

		public string[] GetUsersInRole(string roleName)
		{
			var users = new List<string>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[GetUsersInRole]";
				command.CommandType = CommandType.StoredProcedure;

				var roleNameParam = command.CreateParameter();
				roleNameParam.ParameterName = "@RoleName";
				roleNameParam.DbType = DbType.StringFixedLength;
				roleNameParam.Value = roleName;

				command.Parameters.Add(roleNameParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var role = (string)reader["Login"];
						users.Add(role);
					}
				}
			}

			return users.ToArray();
		}

		public void RemoveUserFromRole(string userName, string roleName)
		{
			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[RemoveUserFromRole]";
				command.CommandType = CommandType.StoredProcedure;

				var userNameParam = command.CreateParameter();
				userNameParam.ParameterName = "@Login";
				userNameParam.DbType = DbType.StringFixedLength;
				userNameParam.Value = userName;

				var roleNameParam = command.CreateParameter();
				roleNameParam.ParameterName = "@RoleName";
				roleNameParam.DbType = DbType.StringFixedLength;
				roleNameParam.Value = roleName;

				command.Parameters.AddRange(new[] { userNameParam, roleNameParam });
				connection.Open();
				command.ExecuteNonQuery();
			}
		}

		public UserModel GetUserInfo(string userName)
		{
			var users = new List<UserModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "[UserInfo].[GetUserInfo]";
				command.CommandType = CommandType.StoredProcedure;

				var userNameParam = command.CreateParameter();
				userNameParam.ParameterName = "@Login";
				userNameParam.DbType = DbType.StringFixedLength;
				userNameParam.Value = userName;

				command.Parameters.Add(userNameParam);
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var user = new UserModel
						{
							UserID = (int)reader["UserID"],
							Login = (string)reader["Login"],
							FirstName = (string)reader["FirstName"],
							LastName = (string)reader["LastName"],
							Password = (Guid)reader["Password"]
						};
						users.Add(user);
					}
				}
			}

			return users.FirstOrDefault();
		}

		public UserModel ValidateUser(string userName, string password)
		{
			var user = GetUserInfo(userName);
			var passwordHash = GetHashString(password);
			var passwordHash1 = GetHashString(password);
			var passwordHash2 = GetHashString(password);

			if (user == null || user.Password != passwordHash)
			{
				return null;
			}

			return user;
		}

		public List<UserModel> GetAllUsers()
		{
			var users = new List<UserModel>();

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString = connectionString;
				var command = connection.CreateCommand();
				command.CommandText = "SELECT * FROM [UserInfo].[User]";
				command.CommandType = CommandType.Text;
				connection.Open();

				using (IDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						var user = new UserModel
						{
							UserID = (int)reader["UserID"],
							FirstName = (string)reader["FirstName"],
							LastName = (string)reader["LastName"],
							Login = (string)reader["Login"],
							Password = (Guid)reader["Password"]
						};
						users.Add(user);
					}
				}
			}

			return users
				.Where(x => !GetRolesForUser(x.Login).Contains("Admin"))
				.ToList();
		}
	}

}
