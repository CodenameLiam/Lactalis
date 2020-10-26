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
using System.Linq;
using Lactalis.Enums;
using TestEnums = EntityObject.Enums;
using ServersideTradingPostListingEntity = Lactalis.Models.TradingPostListingEntity;

namespace APITests.EntityObjects.Models
{
	public class TradingPostListingEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Title { get; set; }
		public String Email { get; set; }
		public String Phone { get; set; }
		public String AdditionalInfo { get; set; }
		public String AddressLine1 { get; set; }
		public String AddressLine2 { get; set; }
		public String PostalCode { get; set; }
		public Guid? ProductImageId { get; set; }
		public int? Price { get; set; }
		public PriceType PriceType { get; set; }

		public Guid? FarmerId { get; set; }
		public ICollection<TradingPostListingsTradingPostCategories> TradingPostCategoriess { get; set; }

		public TradingPostListingEntityDto(TradingPostListingEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			Email = model.Email;
			Phone = model.Phone;
			AdditionalInfo = model.AdditionalInfo;
			AddressLine1 = model.AddressLine1;
			AddressLine2 = model.AddressLine2;
			PostalCode = model.PostalCode;
			ProductImageId = model.ProductImageId;
			Price = model.Price;
			PriceType = (PriceType)model.PriceType;
			FarmerId = model.FarmerId;
			TradingPostCategoriess = model.TradingPostCategoriess;
		}

		public TradingPostListingEntityDto(ServersideTradingPostListingEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Title = model.Title;
			Email = model.Email;
			Phone = model.Phone;
			AdditionalInfo = model.AdditionalInfo;
			AddressLine1 = model.AddressLine1;
			AddressLine2 = model.AddressLine2;
			PostalCode = model.PostalCode;
			ProductImageId = model.ProductImageId;
			Price = model.Price;
			PriceType = model.PriceType;
			FarmerId = model.FarmerId;
			TradingPostCategoriess  = model.TradingPostCategoriess == null ? null :TradingPostListingsTradingPostCategoriesDto.Convert(model.TradingPostCategoriess);
		}

		public TradingPostListingEntity GetTesttargetTradingPostListingEntity()
		{
			return new TradingPostListingEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Title = Title,
				Email = Email,
				Phone = Phone,
				AdditionalInfo = AdditionalInfo,
				AddressLine1 = AddressLine1,
				AddressLine2 = AddressLine2,
				PostalCode = PostalCode,
				ProductImageId = ProductImageId,
				Price = Price,
				PriceType = (TestEnums.PriceType)PriceType,
				FarmerId = FarmerId,
				TradingPostCategoriess = TradingPostCategoriess,
			};
		}

		public ServersideTradingPostListingEntity GetServersideTradingPostListingEntity()
		{
			return new ServersideTradingPostListingEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Title = Title,
				Email = Email,
				Phone = Phone,
				AdditionalInfo = AdditionalInfo,
				AddressLine1 = AddressLine1,
				AddressLine2 = AddressLine2,
				PostalCode = PostalCode,
				ProductImageId = ProductImageId,
				Price = Price,
				PriceType = PriceType,
				FarmerId = FarmerId,
				TradingPostCategoriess = TradingPostCategoriess == null ? null :TradingPostListingsTradingPostCategoriesDto.Convert(TradingPostCategoriess),
			};
		}

		public static ServersideTradingPostListingEntity Convert(TradingPostListingEntity model)
		{
			var dto = new TradingPostListingEntityDto(model);
			return dto.GetServersideTradingPostListingEntity();
		}

		public static TradingPostListingEntity Convert(ServersideTradingPostListingEntity model)
		{
			var dto = new TradingPostListingEntityDto(model);
			return dto.GetTesttargetTradingPostListingEntity();
		}
	}
}