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
using ServersideNewsArticleEntity = Lactalis.Models.NewsArticleEntity;

namespace APITests.EntityObjects.Models
{
	public class NewsArticleEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
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
			PromotedArticlesId = model.PromotedArticlesId;
		}

		public NewsArticleEntityDto(ServersideNewsArticleEntity model)
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
			PromotedArticlesId = model.PromotedArticlesId;
		}

		public NewsArticleEntity GetTesttargetNewsArticleEntity()
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
				PromotedArticlesId = PromotedArticlesId,
			};
		}

		public ServersideNewsArticleEntity GetServersideNewsArticleEntity()
		{
			return new ServersideNewsArticleEntity
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
				PromotedArticlesId = PromotedArticlesId,
			};
		}

		public static ServersideNewsArticleEntity Convert(NewsArticleEntity model)
		{
			var dto = new NewsArticleEntityDto(model);
			return dto.GetServersideNewsArticleEntity();
		}

		public static NewsArticleEntity Convert(ServersideNewsArticleEntity model)
		{
			var dto = new NewsArticleEntityDto(model);
			return dto.GetTesttargetNewsArticleEntity();
		}
	}
}