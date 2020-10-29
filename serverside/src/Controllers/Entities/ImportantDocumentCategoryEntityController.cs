
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
	/// The controller that provides rest endpoints for the ImportantDocumentCategoryEntity model
	/// </summary>
	[Route("/api/entity/ImportantDocumentCategoryEntity")]
	[Authorize]
	[ApiController]
	public class ImportantDocumentCategoryEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public ImportantDocumentCategoryEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the ImportantDocumentCategoryEntity for the given id
		/// </summary>
		/// <param name="id">The id of the ImportantDocumentCategoryEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The ImportantDocumentCategoryEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<ImportantDocumentCategoryEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<ImportantDocumentCategoryEntity>(id);
			return await result
				.Select(model => new ImportantDocumentCategoryEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all ImportantDocumentCategoryEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of ImportantDocumentCategoryEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<ImportantDocumentCategoryEntityDto>> Get(
			[FromQuery]ImportantDocumentCategoryEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<ImportantDocumentCategoryEntityDto>
			{
				Data = await _crudService.Get<ImportantDocumentCategoryEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new ImportantDocumentCategoryEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<ImportantDocumentCategoryEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create ImportantDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The new ImportantDocumentCategoryEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The ImportantDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<ImportantDocumentCategoryEntityDto> Post(
			[BindRequired, FromBody] ImportantDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new ImportantDocumentCategoryEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create ImportantDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ImportantDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<ImportantDocumentCategoryEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<ImportantDocumentCategoryEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new ImportantDocumentCategoryEntityDto(result);
		}

		/// <summary>
		/// Update an ImportantDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The ImportantDocumentCategoryEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ImportantDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<ImportantDocumentCategoryEntityDto> Put(
			[BindRequired, FromBody] ImportantDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new ImportantDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an ImportantDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ImportantDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<ImportantDocumentCategoryEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<ImportantDocumentCategoryEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new ImportantDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a ImportantDocumentCategoryEntity
		/// </summary>
		/// <param name="id">The id of the ImportantDocumentCategoryEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted ImportantDocumentCategoryEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<ImportantDocumentCategoryEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Important Document Categorys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Important Document Categorys</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<ImportantDocumentCategoryEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new ImportantDocumentCategoryEntityDto(r)),
				"export_important_document_category.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Important Document Categorys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Important Document Categorys</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<ImportantDocumentCategoryEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new ImportantDocumentCategoryEntityDto(r)),
				"export_important_document_category.csv",
				cancellationToken);
		}


		public class ImportantDocumentCategoryEntityOptions : PaginationOptions
		{
		}

	}
}

