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
using ServersidePromotedArticlesEntity = Lactalis.Models.PromotedArticlesEntity;

namespace APITests.EntityObjects.Models
{
	public class PromotedArticlesEntityDto
	{
		public Guid Id { get; set; }
		public DateTime Created { get; set; }
		public DateTime Modified { get; set; }
		public State State { get; set; }

		public ICollection<NewsArticleEntity> NewsArticless { get; set; }

		public PromotedArticlesEntityDto(PromotedArticlesEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			State = (State)model.State;
			NewsArticless = model.NewsArticless;
		}

		public PromotedArticlesEntityDto(ServersidePromotedArticlesEntity model)
		{
			Id = model.Id;
			Created = model.Created;
			Modified = model.Modified;
			State = model.State;
			NewsArticless = model.NewsArticless.Select(NewsArticleEntityDto.Convert).ToList();
		}

		public PromotedArticlesEntity GetTesttargetPromotedArticlesEntity()
		{
			return new PromotedArticlesEntity
			{
				Id = Id,
				Created = Created,
				Modified = Modified,
				State = (TestEnums.State)State,
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
				State = State,
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