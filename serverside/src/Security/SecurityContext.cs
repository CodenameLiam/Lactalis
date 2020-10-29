
using System;
using System.Collections.Generic;
using Lactalis.Models;
using Microsoft.AspNetCore.Identity;


namespace Lactalis.Security
{
	public class SecurityContext
	{
		public LactalisDBContext DbContext { get; set; }
		public UserManager<User> UserManager { get; set; }
		public IList<string> Groups { get; set; }
		public IServiceProvider ServiceProvider { get; set; }
	}
}