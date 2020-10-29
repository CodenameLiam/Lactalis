
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
	/// The controller that provides rest endpoints for the AgriSupplyDocumentEntity model
	/// </summary>
	[Route("/api/entity/AgriSupplyDocumentEntity")]
	[Authorize]
	[ApiController]
	public class AgriSupplyDocumentEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public AgriSupplyDocumentEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the AgriSupplyDocumentEntity for the given id
		/// </summary>
		/// <param name="id">The id of the AgriSupplyDocumentEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The AgriSupplyDocumentEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<AgriSupplyDocumentEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<AgriSupplyDocumentEntity>(id);
			return await result
				.Select(model => new AgriSupplyDocumentEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all AgriSupplyDocumentEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of AgriSupplyDocumentEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<AgriSupplyDocumentEntityDto>> Get(
			[FromQuery]AgriSupplyDocumentEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<AgriSupplyDocumentEntityDto>
			{
				Data = await _crudService.Get<AgriSupplyDocumentEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new AgriSupplyDocumentEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<AgriSupplyDocumentEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create AgriSupplyDocumentEntity
		/// </summary>
		/// <param name="model">The new AgriSupplyDocumentEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The AgriSupplyDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<AgriSupplyDocumentEntityDto> Post(
			[BindRequired, FromBody] AgriSupplyDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AgriSupplyDocumentEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create AgriSupplyDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AgriSupplyDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<AgriSupplyDocumentEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<AgriSupplyDocumentEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new AgriSupplyDocumentEntityDto(result);
		}

		/// <summary>
		/// Update an AgriSupplyDocumentEntity
		/// </summary>
		/// <param name="model">The AgriSupplyDocumentEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AgriSupplyDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<AgriSupplyDocumentEntityDto> Put(
			[BindRequired, FromBody] AgriSupplyDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AgriSupplyDocumentEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an AgriSupplyDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AgriSupplyDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<AgriSupplyDocumentEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<AgriSupplyDocumentEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AgriSupplyDocumentEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a AgriSupplyDocumentEntity
		/// </summary>
		/// <param name="id">The id of the AgriSupplyDocumentEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted AgriSupplyDocumentEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<AgriSupplyDocumentEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Agri Supply Documents with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Agri Supply Documents</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<AgriSupplyDocumentEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new AgriSupplyDocumentEntityDto(r)),
				"export_agri_supply_document.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Agri Supply Documents with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Agri Supply Documents</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<AgriSupplyDocumentEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new AgriSupplyDocumentEntityDto(r)),
				"export_agri_supply_document.csv",
				cancellationToken);
		}


		public class AgriSupplyDocumentEntityOptions : PaginationOptions
		{
		}

	}
}

