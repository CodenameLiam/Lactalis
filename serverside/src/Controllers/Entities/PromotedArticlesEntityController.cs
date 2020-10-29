
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
	/// The controller that provides rest endpoints for the PromotedArticlesEntity model
	/// </summary>
	[Route("/api/entity/PromotedArticlesEntity")]
	[Authorize]
	[ApiController]
	public class PromotedArticlesEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public PromotedArticlesEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the PromotedArticlesEntity for the given id
		/// </summary>
		/// <param name="id">The id of the PromotedArticlesEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The PromotedArticlesEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<PromotedArticlesEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<PromotedArticlesEntity>(id);
			return await result
				.Select(model => new PromotedArticlesEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all PromotedArticlesEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of PromotedArticlesEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<PromotedArticlesEntityDto>> Get(
			[FromQuery]PromotedArticlesEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<PromotedArticlesEntityDto>
			{
				Data = await _crudService.Get<PromotedArticlesEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new PromotedArticlesEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<PromotedArticlesEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create PromotedArticlesEntity
		/// </summary>
		/// <param name="model">The new PromotedArticlesEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The PromotedArticlesEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<PromotedArticlesEntityDto> Post(
			[BindRequired, FromBody] PromotedArticlesEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new PromotedArticlesEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create PromotedArticlesEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The PromotedArticlesEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<PromotedArticlesEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<PromotedArticlesEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new PromotedArticlesEntityDto(result);
		}

		/// <summary>
		/// Update an PromotedArticlesEntity
		/// </summary>
		/// <param name="model">The PromotedArticlesEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The PromotedArticlesEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<PromotedArticlesEntityDto> Put(
			[BindRequired, FromBody] PromotedArticlesEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new PromotedArticlesEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an PromotedArticlesEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The PromotedArticlesEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<PromotedArticlesEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<PromotedArticlesEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new PromotedArticlesEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a PromotedArticlesEntity
		/// </summary>
		/// <param name="id">The id of the PromotedArticlesEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted PromotedArticlesEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<PromotedArticlesEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Promoted Articless with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Promoted Articless</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<PromotedArticlesEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new PromotedArticlesEntityDto(r)),
				"export_promoted_articles.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Promoted Articless with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Promoted Articless</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<PromotedArticlesEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new PromotedArticlesEntityDto(r)),
				"export_promoted_articles.csv",
				cancellationToken);
		}


		public class PromotedArticlesEntityOptions : PaginationOptions
		{
		}

	}
}

