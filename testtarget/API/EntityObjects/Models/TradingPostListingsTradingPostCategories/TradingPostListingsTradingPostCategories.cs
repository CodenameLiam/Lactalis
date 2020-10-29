
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lactalis.Security;
using Lactalis.Security.Acl;


namespace APITests.EntityObjects.Models
{
	public class TradingPostListingsTradingPostCategories
	{
		public TradingPostListingsTradingPostCategories() {}

		public Guid Owner { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TradingPostListingEntity"/>
		public Guid TradingPostListingsId { get; set; }
		public TradingPostListingEntity TradingPostListings { get; set; }

		/// <summary>
		/// Outgoing one to many reference
		/// </summary>
		/// <see cref="Lactalis.Models.TradingPostCategoryEntity"/>
		public Guid TradingPostCategoriesId { get; set; }
		public TradingPostCategoryEntity TradingPostCategories { get; set; }
	}
}