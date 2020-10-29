
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
	/// The controller that provides rest endpoints for the ImportantDocumentEntity model
	/// </summary>
	[Route("/api/entity/ImportantDocumentEntity")]
	[Authorize]
	[ApiController]
	public class ImportantDocumentEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public ImportantDocumentEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the ImportantDocumentEntity for the given id
		/// </summary>
		/// <param name="id">The id of the ImportantDocumentEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The ImportantDocumentEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<ImportantDocumentEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<ImportantDocumentEntity>(id);
			return await result
				.Select(model => new ImportantDocumentEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all ImportantDocumentEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of ImportantDocumentEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<ImportantDocumentEntityDto>> Get(
			[FromQuery]ImportantDocumentEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<ImportantDocumentEntityDto>
			{
				Data = await _crudService.Get<ImportantDocumentEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new ImportantDocumentEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<ImportantDocumentEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create ImportantDocumentEntity
		/// </summary>
		/// <param name="model">The new ImportantDocumentEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The ImportantDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<ImportantDocumentEntityDto> Post(
			[BindRequired, FromBody] ImportantDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new ImportantDocumentEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create ImportantDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ImportantDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<ImportantDocumentEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<ImportantDocumentEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new ImportantDocumentEntityDto(result);
		}

		/// <summary>
		/// Update an ImportantDocumentEntity
		/// </summary>
		/// <param name="model">The ImportantDocumentEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ImportantDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<ImportantDocumentEntityDto> Put(
			[BindRequired, FromBody] ImportantDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new ImportantDocumentEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an ImportantDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ImportantDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<ImportantDocumentEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<ImportantDocumentEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new ImportantDocumentEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a ImportantDocumentEntity
		/// </summary>
		/// <param name="id">The id of the ImportantDocumentEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted ImportantDocumentEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<ImportantDocumentEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Important Documents with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Important Documents</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<ImportantDocumentEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new ImportantDocumentEntityDto(r)),
				"export_important_document.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Important Documents with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Important Documents</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<ImportantDocumentEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new ImportantDocumentEntityDto(r)),
				"export_important_document.csv",
				cancellationToken);
		}


		public class ImportantDocumentEntityOptions : PaginationOptions
		{
		}

	}
}

