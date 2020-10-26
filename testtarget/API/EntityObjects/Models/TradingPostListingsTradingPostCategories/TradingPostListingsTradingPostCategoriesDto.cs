/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-licence. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */

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