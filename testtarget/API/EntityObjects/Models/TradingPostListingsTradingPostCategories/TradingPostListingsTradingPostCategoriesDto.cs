

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lactalis.Security;
using Lactalis.Security.Acl;
using ServersideTradingPostListingsTradingPostCategories = Lactalis.Models.TradingPostListingsTradingPostCategories;

namespace APITests.EntityObjects.Models
{
	public class TradingPostListingsTradingPostCategoriesDto
	{
		//public Guid Owner { get; set; }
		public Guid TradingPostListingsId { get; set; }
		public Guid TradingPostCategoriesId { get; set; }

		public TradingPostListingsTradingPostCategoriesDto(TradingPostListingsTradingPostCategories model)
		{
			//Owner = model.Owner;
			TradingPostListingsId = model.TradingPostListingsId;
			TradingPostCategoriesId = model.TradingPostCategoriesId;
		}

		public TradingPostListingsTradingPostCategoriesDto(ServersideTradingPostListingsTradingPostCategories model)
		{
			//Owner = model.Owner;
			TradingPostListingsId = model.TradingPostListingsId;
			TradingPostCategoriesId = model.TradingPostCategoriesId;
		}

		public ServersideTradingPostListingsTradingPostCategories GetServersideTradingPostListingsTradingPostCategories ()
		{
			return new ServersideTradingPostListingsTradingPostCategories()
			{
				//Owner = Owner,
				TradingPostListingsId = TradingPostListingsId,
				TradingPostCategoriesId = TradingPostCategoriesId,
			};
		}

		public TradingPostListingsTradingPostCategories GetTesttargetTradingPostListingsTradingPostCategories ()
		{
			return new TradingPostListingsTradingPostCategories()
			{
				//Owner = Owner,
				TradingPostListingsId = TradingPostListingsId,
				TradingPostCategoriesId = TradingPostCategoriesId,
			};
		}

		public static ICollection<ServersideTradingPostListingsTradingPostCategories> Convert(ICollection<TradingPostListingsTradingPostCategories> collection)
		{
			var newCollection = new List<ServersideTradingPostListingsTradingPostCategories>();


			foreach (var item in collection)
			{
				newCollection.Add(new TradingPostListingsTradingPostCategoriesDto(item).GetServersideTradingPostListingsTradingPostCategories());
			}
			return newCollection;
		}

		public static ICollection<TradingPostListingsTradingPostCategories> Convert(ICollection<ServersideTradingPostListingsTradingPostCategories> collection)
		{
			var newCollection = new List<TradingPostListingsTradingPostCategories>();

			foreach (var item in collection)
			{
				newCollection.Add(new TradingPostListingsTradingPostCategoriesDto(item).GetTesttargetTradingPostListingsTradingPostCategories());
			}
			return newCollection;
		}
	}
}