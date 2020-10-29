
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lactalis.Validators;


namespace Lactalis.Models.RegistrationModels
{
	public class AdminEntityRegistrationModel : AdminEntityDto, IRegistrationModel<AdminEntity>
	{

		[Email]
		[Required]
		public new string Email { get; set; }

		[Required]
		public string Password { get; set; }
		
	
		public IList<string> Groups => new List<string> {
			"Admin",
		};


		public override AdminEntity ToModel()
		{
			var model = base.ToModel();
			model.Email = Email;
			return model;
		}
	}

	public class AdminEntityGraphQlRegistrationModel : AdminEntityRegistrationModel
	{
		public override AdminEntity ToModel()
		{
			var model = base.ToModel();
			return model;
		}
	}
}