
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
	/// The controller that provides rest endpoints for the FarmerEntity model
	/// </summary>
	[Route("/api/entity/FarmerEntity")]
	[Authorize]
	[ApiController]
	public class FarmerEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public FarmerEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the FarmerEntity for the given id
		/// </summary>
		/// <param name="id">The id of the FarmerEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The FarmerEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<FarmerEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<FarmerEntity>(id);
			return await result
				.Select(model => new FarmerEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all FarmerEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of FarmerEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<FarmerEntityDto>> Get(
			[FromQuery]FarmerEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<FarmerEntityDto>
			{
				Data = await _crudService.Get<FarmerEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new FarmerEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<FarmerEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create FarmerEntity
		/// </summary>
		/// <param name="model">The new FarmerEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The FarmerEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<FarmerEntityDto> Post(
			[BindRequired, FromBody] FarmerEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new FarmerEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create FarmerEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The FarmerEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<FarmerEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<FarmerEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new FarmerEntityDto(result);
		}

		/// <summary>
		/// Update an FarmerEntity
		/// </summary>
		/// <param name="model">The FarmerEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The FarmerEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<FarmerEntityDto> Put(
			[BindRequired, FromBody] FarmerEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new FarmerEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an FarmerEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The FarmerEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<FarmerEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<FarmerEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new FarmerEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a FarmerEntity
		/// </summary>
		/// <param name="id">The id of the FarmerEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted FarmerEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<FarmerEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Farmers with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Farmers</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<FarmerEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new FarmerEntityDto(r)),
				"export_farmer.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Farmers with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Farmers</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<FarmerEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new FarmerEntityDto(r)),
				"export_farmer.csv",
				cancellationToken);
		}


		public class FarmerEntityOptions : PaginationOptions
		{
		}

	}
}

