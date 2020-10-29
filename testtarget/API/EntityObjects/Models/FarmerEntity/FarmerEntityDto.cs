
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideFarmerEntity = Lactalis.Models.FarmerEntity;

namespace APITests.EntityObjects.Models
{
	public class FarmerEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }

		public ICollection<TradingPostListingEntity> TradingPostListingss { get; set; }
		public ICollection<FarmersFarms> Farmss { get; set; }

		public FarmerEntityDto(FarmerEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			TradingPostListingss = model.TradingPostListingss;
			Farmss = model.Farmss;
		}

		public FarmerEntityDto(ServersideFarmerEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			TradingPostListingss = model.TradingPostListingss.Select(TradingPostListingEntityDto.Convert).ToList();
			Farmss  = model.Farmss == null ? null :FarmersFarmsDto.Convert(model.Farmss);
		}

		public FarmerEntity GetTesttargetFarmerEntity()
		{
			return new FarmerEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				TradingPostListingss = TradingPostListingss,
				Farmss = Farmss,
			};
		}

		public ServersideFarmerEntity GetServersideFarmerEntity()
		{
			return new ServersideFarmerEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				TradingPostListingss = TradingPostListingss?.Select(TradingPostListingEntityDto.Convert).ToList(),
				Farmss = Farmss == null ? null :FarmersFarmsDto.Convert(Farmss),
			};
		}

		public static ServersideFarmerEntity Convert(FarmerEntity model)
		{
			var dto = new FarmerEntityDto(model);
			return dto.GetServersideFarmerEntity();
		}

		public static FarmerEntity Convert(ServersideFarmerEntity model)
		{
			var dto = new FarmerEntityDto(model);
			return dto.GetTesttargetFarmerEntity();
		}
	}
}