
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
	/// The controller that provides rest endpoints for the TradingPostListingEntity model
	/// </summary>
	[Route("/api/entity/TradingPostListingEntity")]
	[Authorize]
	[ApiController]
	public class TradingPostListingEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public TradingPostListingEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the TradingPostListingEntity for the given id
		/// </summary>
		/// <param name="id">The id of the TradingPostListingEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The TradingPostListingEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<TradingPostListingEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<TradingPostListingEntity>(id);
			return await result
				.Select(model => new TradingPostListingEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all TradingPostListingEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of TradingPostListingEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<TradingPostListingEntityDto>> Get(
			[FromQuery]TradingPostListingEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<TradingPostListingEntityDto>
			{
				Data = await _crudService.Get<TradingPostListingEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new TradingPostListingEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<TradingPostListingEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create TradingPostListingEntity
		/// </summary>
		/// <param name="model">The new TradingPostListingEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The TradingPostListingEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<TradingPostListingEntityDto> Post(
			[BindRequired, FromBody] TradingPostListingEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TradingPostListingEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create TradingPostListingEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TradingPostListingEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<TradingPostListingEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<TradingPostListingEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new TradingPostListingEntityDto(result);
		}

		/// <summary>
		/// Update an TradingPostListingEntity
		/// </summary>
		/// <param name="model">The TradingPostListingEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TradingPostListingEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<TradingPostListingEntityDto> Put(
			[BindRequired, FromBody] TradingPostListingEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TradingPostListingEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an TradingPostListingEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TradingPostListingEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<TradingPostListingEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<TradingPostListingEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TradingPostListingEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a TradingPostListingEntity
		/// </summary>
		/// <param name="id">The id of the TradingPostListingEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted TradingPostListingEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<TradingPostListingEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Trading Post Listings with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Trading Post Listings</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<TradingPostListingEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new TradingPostListingEntityDto(r)),
				"export_trading_post_listing.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Trading Post Listings with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Trading Post Listings</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<TradingPostListingEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new TradingPostListingEntityDto(r)),
				"export_trading_post_listing.csv",
				cancellationToken);
		}


		public class TradingPostListingEntityOptions : PaginationOptions
		{
		}

	}
}

