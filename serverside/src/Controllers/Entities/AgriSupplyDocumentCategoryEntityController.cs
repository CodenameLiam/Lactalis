
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
	/// The controller that provides rest endpoints for the AgriSupplyDocumentCategoryEntity model
	/// </summary>
	[Route("/api/entity/AgriSupplyDocumentCategoryEntity")]
	[Authorize]
	[ApiController]
	public class AgriSupplyDocumentCategoryEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public AgriSupplyDocumentCategoryEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the AgriSupplyDocumentCategoryEntity for the given id
		/// </summary>
		/// <param name="id">The id of the AgriSupplyDocumentCategoryEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The AgriSupplyDocumentCategoryEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<AgriSupplyDocumentCategoryEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<AgriSupplyDocumentCategoryEntity>(id);
			return await result
				.Select(model => new AgriSupplyDocumentCategoryEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all AgriSupplyDocumentCategoryEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of AgriSupplyDocumentCategoryEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<AgriSupplyDocumentCategoryEntityDto>> Get(
			[FromQuery]AgriSupplyDocumentCategoryEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<AgriSupplyDocumentCategoryEntityDto>
			{
				Data = await _crudService.Get<AgriSupplyDocumentCategoryEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new AgriSupplyDocumentCategoryEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<AgriSupplyDocumentCategoryEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create AgriSupplyDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The new AgriSupplyDocumentCategoryEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The AgriSupplyDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<AgriSupplyDocumentCategoryEntityDto> Post(
			[BindRequired, FromBody] AgriSupplyDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AgriSupplyDocumentCategoryEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create AgriSupplyDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AgriSupplyDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<AgriSupplyDocumentCategoryEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<AgriSupplyDocumentCategoryEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new AgriSupplyDocumentCategoryEntityDto(result);
		}

		/// <summary>
		/// Update an AgriSupplyDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The AgriSupplyDocumentCategoryEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AgriSupplyDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<AgriSupplyDocumentCategoryEntityDto> Put(
			[BindRequired, FromBody] AgriSupplyDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AgriSupplyDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an AgriSupplyDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AgriSupplyDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<AgriSupplyDocumentCategoryEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<AgriSupplyDocumentCategoryEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AgriSupplyDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a AgriSupplyDocumentCategoryEntity
		/// </summary>
		/// <param name="id">The id of the AgriSupplyDocumentCategoryEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted AgriSupplyDocumentCategoryEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<AgriSupplyDocumentCategoryEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Agri Supply Document Categorys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Agri Supply Document Categorys</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<AgriSupplyDocumentCategoryEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new AgriSupplyDocumentCategoryEntityDto(r)),
				"export_agri_supply_document_category.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Agri Supply Document Categorys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Agri Supply Document Categorys</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<AgriSupplyDocumentCategoryEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new AgriSupplyDocumentCategoryEntityDto(r)),
				"export_agri_supply_document_category.csv",
				cancellationToken);
		}


		public class AgriSupplyDocumentCategoryEntityOptions : PaginationOptions
		{
		}

	}
}

