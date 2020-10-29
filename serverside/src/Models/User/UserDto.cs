
using System;

namespace Lactalis.Models
{
	public class UserDto : ModelDto<User>
	{
		public Guid Owner { get; set; }

		public string UserName { get; set; }

		public string Email { get; set; }

		public bool EmailConfirmed { get; set; }

		public string Discriminator { get; set; }


		public UserDto(User model)
		{
			LoadModelData(model);
		}

		public UserDto() { }

		public override User ToModel()
		{
			return new User
			{
				Id = Id,
				UserName = UserName,
				Email = Email,
				EmailConfirmed = EmailConfirmed,
				Discriminator = Discriminator,
			};
		}

		public override ModelDto<User> LoadModelData(User model)
		{
			Id = model.Id;
			UserName = model.UserName;
			Email = model.Email;
			EmailConfirmed = model.EmailConfirmed;
			Discriminator = model.Discriminator;
			return this;
		}
	}
}