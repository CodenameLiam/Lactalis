
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
	/// The controller that provides rest endpoints for the QualityDocumentEntity model
	/// </summary>
	[Route("/api/entity/QualityDocumentEntity")]
	[Authorize]
	[ApiController]
	public class QualityDocumentEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public QualityDocumentEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the QualityDocumentEntity for the given id
		/// </summary>
		/// <param name="id">The id of the QualityDocumentEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The QualityDocumentEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<QualityDocumentEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<QualityDocumentEntity>(id);
			return await result
				.Select(model => new QualityDocumentEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all QualityDocumentEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of QualityDocumentEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<QualityDocumentEntityDto>> Get(
			[FromQuery]QualityDocumentEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<QualityDocumentEntityDto>
			{
				Data = await _crudService.Get<QualityDocumentEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new QualityDocumentEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<QualityDocumentEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create QualityDocumentEntity
		/// </summary>
		/// <param name="model">The new QualityDocumentEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The QualityDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<QualityDocumentEntityDto> Post(
			[BindRequired, FromBody] QualityDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new QualityDocumentEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create QualityDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The QualityDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<QualityDocumentEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<QualityDocumentEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new QualityDocumentEntityDto(result);
		}

		/// <summary>
		/// Update an QualityDocumentEntity
		/// </summary>
		/// <param name="model">The QualityDocumentEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The QualityDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<QualityDocumentEntityDto> Put(
			[BindRequired, FromBody] QualityDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new QualityDocumentEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an QualityDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The QualityDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<QualityDocumentEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<QualityDocumentEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new QualityDocumentEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a QualityDocumentEntity
		/// </summary>
		/// <param name="id">The id of the QualityDocumentEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted QualityDocumentEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<QualityDocumentEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Quality Documents with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Quality Documents</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<QualityDocumentEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new QualityDocumentEntityDto(r)),
				"export_quality_document.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Quality Documents with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Quality Documents</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<QualityDocumentEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new QualityDocumentEntityDto(r)),
				"export_quality_document.csv",
				cancellationToken);
		}


		public class QualityDocumentEntityOptions : PaginationOptions
		{
		}

	}
}

