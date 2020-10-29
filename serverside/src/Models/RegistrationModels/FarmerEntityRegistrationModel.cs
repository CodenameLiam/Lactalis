
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lactalis.Validators;


namespace Lactalis.Models.RegistrationModels
{
	public class FarmerEntityRegistrationModel : FarmerEntityDto, IRegistrationModel<FarmerEntity>
	{

		[Email]
		[Required]
		public new string Email { get; set; }

		[Required]
		public string Password { get; set; }
		
	
		public IList<string> Groups => new List<string> {
			"Farmer",
		};


		public override FarmerEntity ToModel()
		{
			var model = base.ToModel();
			model.Email = Email;
			return model;
		}
	}

	public class FarmerEntityGraphQlRegistrationModel : FarmerEntityRegistrationModel
	{
		public ICollection<TradingPostListingEntity> TradingPostListingss { get; set; }

		public ICollection<FarmersFarms> Farmss { get; set; }

		public override FarmerEntity ToModel()
		{
			var model = base.ToModel();
			model.TradingPostListingss = TradingPostListingss;
			model.Farmss = Farmss;
			return model;
		}
	}
}