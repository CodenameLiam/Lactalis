
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Helpers;
using Lactalis.Models;
using Lactalis.Services;
using Lactalis.Services.Interfaces;
using GraphQL.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
 

namespace Lactalis.Controllers.Entities
{
	/// <summary>
	/// The controller that provides rest endpoints for the NewsArticleEntity model
	/// </summary>
	[Route("/api/entity/NewsArticleEntity")]
	[Authorize]
	[ApiController]
	public class NewsArticleEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public NewsArticleEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the NewsArticleEntity for the given id
		/// </summary>
		/// <param name="id">The id of the NewsArticleEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The NewsArticleEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<NewsArticleEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<NewsArticleEntity>(id);
			return await result
				.Select(model => new NewsArticleEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all NewsArticleEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of NewsArticleEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<NewsArticleEntityDto>> Get(
			[FromQuery]NewsArticleEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<NewsArticleEntityDto>
			{
				Data = await _crudService.Get<NewsArticleEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new NewsArticleEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<NewsArticleEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create NewsArticleEntity
		/// </summary>
		/// <param name="model">The new NewsArticleEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The NewsArticleEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<NewsArticleEntityDto> Post(
			[BindRequired, FromBody] NewsArticleEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new NewsArticleEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create NewsArticleEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The NewsArticleEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<NewsArticleEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<NewsArticleEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new NewsArticleEntityDto(result);
		}

		/// <summary>
		/// Update an NewsArticleEntity
		/// </summary>
		/// <param name="model">The NewsArticleEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The NewsArticleEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<NewsArticleEntityDto> Put(
			[BindRequired, FromBody] NewsArticleEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new NewsArticleEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an NewsArticleEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The NewsArticleEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<NewsArticleEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<NewsArticleEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new NewsArticleEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a NewsArticleEntity
		/// </summary>
		/// <param name="id">The id of the NewsArticleEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted NewsArticleEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<NewsArticleEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of News Articles with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of News Articles</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<NewsArticleEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new NewsArticleEntityDto(r)),
				"export_news_article.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of News Articles with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of News Articles</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<NewsArticleEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new NewsArticleEntityDto(r)),
				"export_news_article.csv",
				cancellationToken);
		}


		public class NewsArticleEntityOptions : PaginationOptions
		{
		}

	}
}

