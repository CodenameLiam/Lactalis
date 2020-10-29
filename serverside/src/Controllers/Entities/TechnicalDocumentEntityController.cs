
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
	/// The controller that provides rest endpoints for the TechnicalDocumentEntity model
	/// </summary>
	[Route("/api/entity/TechnicalDocumentEntity")]
	[Authorize]
	[ApiController]
	public class TechnicalDocumentEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public TechnicalDocumentEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the TechnicalDocumentEntity for the given id
		/// </summary>
		/// <param name="id">The id of the TechnicalDocumentEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The TechnicalDocumentEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<TechnicalDocumentEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<TechnicalDocumentEntity>(id);
			return await result
				.Select(model => new TechnicalDocumentEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all TechnicalDocumentEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of TechnicalDocumentEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<TechnicalDocumentEntityDto>> Get(
			[FromQuery]TechnicalDocumentEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<TechnicalDocumentEntityDto>
			{
				Data = await _crudService.Get<TechnicalDocumentEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new TechnicalDocumentEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<TechnicalDocumentEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create TechnicalDocumentEntity
		/// </summary>
		/// <param name="model">The new TechnicalDocumentEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The TechnicalDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<TechnicalDocumentEntityDto> Post(
			[BindRequired, FromBody] TechnicalDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TechnicalDocumentEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create TechnicalDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TechnicalDocumentEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<TechnicalDocumentEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<TechnicalDocumentEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new TechnicalDocumentEntityDto(result);
		}

		/// <summary>
		/// Update an TechnicalDocumentEntity
		/// </summary>
		/// <param name="model">The TechnicalDocumentEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TechnicalDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<TechnicalDocumentEntityDto> Put(
			[BindRequired, FromBody] TechnicalDocumentEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TechnicalDocumentEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an TechnicalDocumentEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The TechnicalDocumentEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<TechnicalDocumentEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<TechnicalDocumentEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new TechnicalDocumentEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a TechnicalDocumentEntity
		/// </summary>
		/// <param name="id">The id of the TechnicalDocumentEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted TechnicalDocumentEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<TechnicalDocumentEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Technical Documents with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Technical Documents</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<TechnicalDocumentEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new TechnicalDocumentEntityDto(r)),
				"export_technical_document.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Technical Documents with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Technical Documents</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<TechnicalDocumentEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new TechnicalDocumentEntityDto(r)),
				"export_technical_document.csv",
				cancellationToken);
		}


		public class TechnicalDocumentEntityOptions : PaginationOptions
		{
		}

	}
}

