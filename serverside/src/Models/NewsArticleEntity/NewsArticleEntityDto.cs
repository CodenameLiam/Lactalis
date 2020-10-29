
using System;
using System.Linq;
using System.Collections.Generic;


namespace Lactalis.Models
{
	public class NewsArticleEntityDto : ModelDto<NewsArticleEntity>
	{
		public String Headline { get; set; }
		public String Description { get; set; }
		public Guid? FeatureImageId { get; set; }
		public String Content { get; set; }
		public Boolean? Qld { get; set; }
		public Boolean? Nsw { get; set; }
		public Boolean? Vic { get; set; }
		public Boolean? Tas { get; set; }
		public Boolean? Wa { get; set; }
		public Boolean? Sa { get; set; }
		public Boolean? Nt { get; set; }

		public Guid? PromotedArticlesId { get; set; }


		public NewsArticleEntityDto(NewsArticleEntity model)
		{
			LoadModelData(model);
		}

		public NewsArticleEntityDto()
		{
		}

		public override NewsArticleEntity ToModel()
		{

			return new NewsArticleEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Headline = Headline,
				Description = Description,
				FeatureImageId = FeatureImageId,
				Content = Content,
				Qld = Qld,
				Nsw = Nsw,
				Vic = Vic,
				Tas = Tas,
				Wa = Wa,
				Sa = Sa,
				Nt = Nt,
				PromotedArticlesId  = PromotedArticlesId,
			};
		}

		public override ModelDto<NewsArticleEntity> LoadModelData(NewsArticleEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Headline = model.Headline;
			Description = model.Description;
			FeatureImageId = model.FeatureImageId;
			Content = model.Content;
			Qld = model.Qld;
			Nsw = model.Nsw;
			Vic = model.Vic;
			Tas = model.Tas;
			Wa = model.Wa;
			Sa = model.Sa;
			Nt = model.Nt;
			PromotedArticlesId  = model.PromotedArticlesId;


			return this;
		}
	}
}