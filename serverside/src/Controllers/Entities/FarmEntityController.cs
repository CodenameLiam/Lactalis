
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
	/// The controller that provides rest endpoints for the FarmEntity model
	/// </summary>
	[Route("/api/entity/FarmEntity")]
	[Authorize]
	[ApiController]
	public class FarmEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public FarmEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the FarmEntity for the given id
		/// </summary>
		/// <param name="id">The id of the FarmEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The FarmEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<FarmEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<FarmEntity>(id);
			return await result
				.Select(model => new FarmEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all FarmEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of FarmEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<FarmEntityDto>> Get(
			[FromQuery]FarmEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<FarmEntityDto>
			{
				Data = await _crudService.Get<FarmEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new FarmEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<FarmEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create FarmEntity
		/// </summary>
		/// <param name="model">The new FarmEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The FarmEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<FarmEntityDto> Post(
			[BindRequired, FromBody] FarmEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new FarmEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create FarmEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The FarmEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<FarmEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<FarmEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new FarmEntityDto(result);
		}

		/// <summary>
		/// Update an FarmEntity
		/// </summary>
		/// <param name="model">The FarmEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The FarmEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<FarmEntityDto> Put(
			[BindRequired, FromBody] FarmEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new FarmEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an FarmEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The FarmEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<FarmEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<FarmEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new FarmEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a FarmEntity
		/// </summary>
		/// <param name="id">The id of the FarmEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted FarmEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<FarmEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Farms with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Farms</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<FarmEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new FarmEntityDto(r)),
				"export_farm.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Farms with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Farms</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<FarmEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new FarmEntityDto(r)),
				"export_farm.csv",
				cancellationToken);
		}


		public class FarmEntityOptions : PaginationOptions
		{
		}

	}
}

