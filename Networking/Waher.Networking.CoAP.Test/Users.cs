﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Waher.Security;

namespace Waher.Networking.CoAP.Test
{
	internal class Users : IUserSource
	{
		private Dictionary<string, IUser> users;

		public Users(params IUser[] Users)
		{
			this.users = new Dictionary<string, IUser>();

			foreach (IUser User in Users)
				this.users[User.UserName] = User;
		}

		public bool TryGetUser(string UserName, out IUser User)
		{
			return this.users.TryGetValue(UserName, out User);
		}
	}
}
