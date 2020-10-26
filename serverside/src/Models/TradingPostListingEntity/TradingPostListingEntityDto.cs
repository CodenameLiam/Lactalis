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
using System.Linq;
using System.Collections.Generic;
using Lactalis.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
// % protected region % [Add any extra imports here] off begin
// % protected region % [Add any extra imports here] end

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

		// % protected region % [Add any extra attributes here] off begin
		// % protected region % [Add any extra attributes here] end

		public TradingPostListingEntityDto(TradingPostListingEntity model)
		{
			LoadModelData(model);
			// % protected region % [Add any constructor logic here] off begin
			// % protected region % [Add any constructor logic here] end
		}

		public TradingPostListingEntityDto()
		{
			// % protected region % [Add any parameterless constructor logic here] off begin
			// % protected region % [Add any parameterless constructor logic here] end
		}

		public override TradingPostListingEntity ToModel()
		{
			// % protected region % [Add any extra ToModel logic here] off begin
			// % protected region % [Add any extra ToModel logic here] end

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
				// % protected region % [Add any extra model properties here] off begin
				// % protected region % [Add any extra model properties here] end
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

			// % protected region % [Add any extra loading data logic here] off begin
			// % protected region % [Add any extra loading data logic here] end

			return this;
		}
	}
}