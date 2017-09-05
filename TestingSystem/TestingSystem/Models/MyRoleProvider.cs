using DataAccessLayer;
using System;
using System.Linq;
using System.Web.Security;

namespace TestingSystem.Models
{
	public class MyRoleProvider : RoleProvider
	{
		public override string[] GetRolesForUser(string username)
		{
			var userRepo = new UsersRepository();
			return userRepo.GetRolesForUser(username);
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			var userRepo = new UsersRepository();
			
			foreach (var userName in usernames)
			{
				foreach (var roleName in roleNames)
				{
					userRepo.AddUserToRole(userName, roleName);
				}
			}
		}

		public override string ApplicationName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			var userRepo = new UsersRepository();
			userRepo.CreateRole(roleName);
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			var userRepo = new UsersRepository();
			userRepo.DeleteRole(roleName);
			return true;
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			var userRepo = new UsersRepository();
			return userRepo.GetUsersInRole(roleName)
				.Where(x => x.Contains(usernameToMatch))
				.ToArray();
		}

		public override string[] GetAllRoles()
		{
			var userRepo = new UsersRepository();
			return userRepo.GetAllRoles();
		}

		public override string[] GetUsersInRole(string roleName)
		{
			var userRepo = new UsersRepository();
			return userRepo.GetUsersInRole(roleName);
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			var userRepo = new UsersRepository();
			return userRepo.GetUsersInRole(roleName).Contains(username);
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName)
		{
			var userRepo = new UsersRepository();
			return userRepo.GetAllRoles().Contains(roleName);
		}
	}
}