
using System.Collections.Generic;
using System;



namespace Lactalis.Models.RegistrationModels
{
	public interface IRegistrationModel<T>
		where T : User
	{
		string Email { get; set; }
		string Password { get; set; }

		IList<string> Groups { get; }


		T ToModel();
	}
}