
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
	/// The controller that provides rest endpoints for the MilkTestEntity model
	/// </summary>
	[Route("/api/entity/MilkTestEntity")]
	[Authorize]
	[ApiController]
	public class MilkTestEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public MilkTestEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the MilkTestEntity for the given id
		/// </summary>
		/// <param name="id">The id of the MilkTestEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The MilkTestEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<MilkTestEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<MilkTestEntity>(id);
			return await result
				.Select(model => new MilkTestEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all MilkTestEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of MilkTestEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<MilkTestEntityDto>> Get(
			[FromQuery]MilkTestEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<MilkTestEntityDto>
			{
				Data = await _crudService.Get<MilkTestEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new MilkTestEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<MilkTestEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create MilkTestEntity
		/// </summary>
		/// <param name="model">The new MilkTestEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The MilkTestEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<MilkTestEntityDto> Post(
			[BindRequired, FromBody] MilkTestEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new MilkTestEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create MilkTestEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The MilkTestEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<MilkTestEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<MilkTestEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new MilkTestEntityDto(result);
		}

		/// <summary>
		/// Update an MilkTestEntity
		/// </summary>
		/// <param name="model">The MilkTestEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The MilkTestEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<MilkTestEntityDto> Put(
			[BindRequired, FromBody] MilkTestEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new MilkTestEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an MilkTestEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The MilkTestEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<MilkTestEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<MilkTestEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new MilkTestEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a MilkTestEntity
		/// </summary>
		/// <param name="id">The id of the MilkTestEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted MilkTestEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<MilkTestEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Milk Tests with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Milk Tests</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<MilkTestEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new MilkTestEntityDto(r)),
				"export_milk_test.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Milk Tests with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Milk Tests</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<MilkTestEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new MilkTestEntityDto(r)),
				"export_milk_test.csv",
				cancellationToken);
		}


		public class MilkTestEntityOptions : PaginationOptions
		{
		}

	}
}

