
using System;
using System.Linq;
using System.Collections.Generic;
using Lactalis.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace Lactalis.Models
{
	public class TradingPostListingEntityDto : ModelDto<TradingPostListingEntity>
	{
		public String Title { get; set; }
		public String Email { get; set; }
		public String Phone { get; set; }
		public String AdditionalInfo { get; set; }
		public String AddressLine1 { get; set; }
		public String AddressLine2 { get; set; }
		public String PostalCode { get; set; }
		public Guid? ProductImageId { get; set; }
		public int? Price { get; set; }
		[JsonProperty("priceType")]
		[JsonConverter(typeof(StringEnumConverter))]
		public PriceType PriceType { get; set; }

		public Guid? FarmerId { get; set; }


		public TradingPostListingEntityDto(TradingPostListingEntity model)
		{
			LoadModelData(model);
		}

		public TradingPostListingEntityDto()
		{
		}

		public override TradingPostListingEntity ToModel()
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
				PriceType = PriceType,
				FarmerId  = FarmerId,
			};
		}

		public override ModelDto<TradingPostListingEntity> LoadModelData(TradingPostListingEntity model)
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
			FarmerId  = model.FarmerId;


			return this;
		}
	}
}