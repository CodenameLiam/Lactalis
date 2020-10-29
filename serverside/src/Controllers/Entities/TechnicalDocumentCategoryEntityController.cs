
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
	/// The controller that provides rest endpoints for the TechnicalDocumentCategoryEntity model
	/// </summary>
	[Route("/api/entity/TechnicalDocumentCategoryEntity")]
	[Authorize]
	[ApiController]
	public class TechnicalDocumentCategoryEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public TechnicalDocumentCategoryEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the TechnicalDocumentCategoryEntity for the given id
		/// </summary>
		/// <param name="id">The id of the TechnicalDocumentCategoryEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The TechnicalDocumentCategoryEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<TechnicalDocumentCategoryEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<TechnicalDocumentCategoryEntity>(id);
			return await result
				.Select(model => new TechnicalDocumentCategoryEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all TechnicalDocumentCategoryEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of TechnicalDocumentCategoryEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<TechnicalDocumentCategoryEntityDto>> Get(
			[FromQuery]TechnicalDocumentCategoryEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<TechnicalDocumentCategoryEntityDto>
			{
				Data = await _crudService.Get<TechnicalDocumentCategoryEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new TechnicalDocumentCategoryEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<TechnicalDocumentCategoryEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create TechnicalDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The new TechnicalDocumentCategoryEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The TechnicalDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<TechnicalDocumentCategoryEntityDto> Post(
			[BindRequired, FromBody] TechnicalDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TechnicalDocumentCategoryEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create TechnicalDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TechnicalDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<TechnicalDocumentCategoryEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<TechnicalDocumentCategoryEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new TechnicalDocumentCategoryEntityDto(result);
		}

		/// <summary>
		/// Update an TechnicalDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The TechnicalDocumentCategoryEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TechnicalDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<TechnicalDocumentCategoryEntityDto> Put(
			[BindRequired, FromBody] TechnicalDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TechnicalDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an TechnicalDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TechnicalDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<TechnicalDocumentCategoryEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<TechnicalDocumentCategoryEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TechnicalDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a TechnicalDocumentCategoryEntity
		/// </summary>
		/// <param name="id">The id of the TechnicalDocumentCategoryEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted TechnicalDocumentCategoryEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<TechnicalDocumentCategoryEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Technical Document Categorys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Technical Document Categorys</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<TechnicalDocumentCategoryEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new TechnicalDocumentCategoryEntityDto(r)),
				"export_technical_document_category.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Technical Document Categorys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Technical Document Categorys</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<TechnicalDocumentCategoryEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new TechnicalDocumentCategoryEntityDto(r)),
				"export_technical_document_category.csv",
				cancellationToken);
		}


		public class TechnicalDocumentCategoryEntityOptions : PaginationOptions
		{
		}

	}
}

