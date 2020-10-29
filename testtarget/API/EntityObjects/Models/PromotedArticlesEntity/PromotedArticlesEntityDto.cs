
using System;
using System.Collections.Generic;
using System.Linq;
using ServersidePromotedArticlesEntity = Lactalis.Models.PromotedArticlesEntity;

namespace APITests.EntityObjects.Models
{
	public class PromotedArticlesEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public String Name { get; set; }

		public ICollection<NewsArticleEntity> NewsArticless { get; set; }

		public PromotedArticlesEntityDto(PromotedArticlesEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			NewsArticless = model.NewsArticless;
		}

		public PromotedArticlesEntityDto(ServersidePromotedArticlesEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			Name = model.Name;
			NewsArticless = model.NewsArticless.Select(NewsArticleEntityDto.Convert).ToList();
		}

		public PromotedArticlesEntity GetTesttargetPromotedArticlesEntity()
		{
			return new PromotedArticlesEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				NewsArticless = NewsArticless,
			};
		}

		public ServersidePromotedArticlesEntity GetServersidePromotedArticlesEntity()
		{
			return new ServersidePromotedArticlesEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				Name = Name,
				NewsArticless = NewsArticless?.Select(NewsArticleEntityDto.Convert).ToList(),
			};
		}

		public static ServersidePromotedArticlesEntity Convert(PromotedArticlesEntity model)
		{
			var dto = new PromotedArticlesEntityDto(model);
			return dto.GetServersidePromotedArticlesEntity();
		}

		public static PromotedArticlesEntity Convert(ServersidePromotedArticlesEntity model)
		{
			var dto = new PromotedArticlesEntityDto(model);
			return dto.GetTesttargetPromotedArticlesEntity();
		}
	}
}