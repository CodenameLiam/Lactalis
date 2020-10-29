
using System;
using System.Collections.Generic;
using System.Linq;
using ServersideTradingPostCategoryEntity = Lactalis.Models.TradingPostCategoryEntity;

namespace APITests.EntityObjects.Models
{
	public class TradingPostCategoryEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<TradingPostListingsTradingPostCategories> TradingPostListingss { get; set; }

		public TradingPostCategoryEntityDto(TradingPostCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			TradingPostListingss = model.TradingPostListingss;
		}

		public TradingPostCategoryEntityDto(ServersideTradingPostCategoryEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			TradingPostListingss  = model.TradingPostListingss == null ? null :TradingPostListingsTradingPostCategoriesDto.Convert(model.TradingPostListingss);
		}

		public TradingPostCategoryEntity GetTesttargetTradingPostCategoryEntity()
		{
			return new TradingPostCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				TradingPostListingss = TradingPostListingss,
			};
		}

		public ServersideTradingPostCategoryEntity GetServersideTradingPostCategoryEntity()
		{
			return new ServersideTradingPostCategoryEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				TradingPostListingss = TradingPostListingss == null ? null :TradingPostListingsTradingPostCategoriesDto.Convert(TradingPostListingss),
			};
		}

		public static ServersideTradingPostCategoryEntity Convert(TradingPostCategoryEntity model)
		{
			var dto = new TradingPostCategoryEntityDto(model);
			return dto.GetServersideTradingPostCategoryEntity();
		}

		public static TradingPostCategoryEntity Convert(ServersideTradingPostCategoryEntity model)
		{
			var dto = new TradingPostCategoryEntityDto(model);
			return dto.GetTesttargetTradingPostCategoryEntity();
		}
	}
}