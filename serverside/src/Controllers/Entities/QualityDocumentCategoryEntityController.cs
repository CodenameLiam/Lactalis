
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
	/// The controller that provides rest endpoints for the QualityDocumentCategoryEntity model
	/// </summary>
	[Route("/api/entity/QualityDocumentCategoryEntity")]
	[Authorize]
	[ApiController]
	public class QualityDocumentCategoryEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public QualityDocumentCategoryEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the QualityDocumentCategoryEntity for the given id
		/// </summary>
		/// <param name="id">The id of the QualityDocumentCategoryEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The QualityDocumentCategoryEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<QualityDocumentCategoryEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<QualityDocumentCategoryEntity>(id);
			return await result
				.Select(model => new QualityDocumentCategoryEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all QualityDocumentCategoryEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of QualityDocumentCategoryEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<QualityDocumentCategoryEntityDto>> Get(
			[FromQuery]QualityDocumentCategoryEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<QualityDocumentCategoryEntityDto>
			{
				Data = await _crudService.Get<QualityDocumentCategoryEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new QualityDocumentCategoryEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<QualityDocumentCategoryEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create QualityDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The new QualityDocumentCategoryEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The QualityDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<QualityDocumentCategoryEntityDto> Post(
			[BindRequired, FromBody] QualityDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new QualityDocumentCategoryEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create QualityDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The QualityDocumentCategoryEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<QualityDocumentCategoryEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<QualityDocumentCategoryEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new QualityDocumentCategoryEntityDto(result);
		}

		/// <summary>
		/// Update an QualityDocumentCategoryEntity
		/// </summary>
		/// <param name="model">The QualityDocumentCategoryEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The QualityDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<QualityDocumentCategoryEntityDto> Put(
			[BindRequired, FromBody] QualityDocumentCategoryEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new QualityDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an QualityDocumentCategoryEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The QualityDocumentCategoryEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<QualityDocumentCategoryEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<QualityDocumentCategoryEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new QualityDocumentCategoryEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a QualityDocumentCategoryEntity
		/// </summary>
		/// <param name="id">The id of the QualityDocumentCategoryEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted QualityDocumentCategoryEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<QualityDocumentCategoryEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Quality Document Categorys with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Quality Document Categorys</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<QualityDocumentCategoryEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new QualityDocumentCategoryEntityDto(r)),
				"export_quality_document_category.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Quality Document Categorys with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Quality Document Categorys</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<QualityDocumentCategoryEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new QualityDocumentCategoryEntityDto(r)),
				"export_quality_document_category.csv",
				cancellationToken);
		}


		public class QualityDocumentCategoryEntityOptions : PaginationOptions
		{
		}

	}
}

