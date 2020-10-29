
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
	/// The controller that provides rest endpoints for the SustainabilityPostEntity model
	/// </summary>
	[Route("/api/entity/SustainabilityPostEntity")]
	[Authorize]
	[ApiController]
	public class SustainabilityPostEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public SustainabilityPostEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the SustainabilityPostEntity for the given id
		/// </summary>
		/// <param name="id">The id of the SustainabilityPostEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The SustainabilityPostEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<SustainabilityPostEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<SustainabilityPostEntity>(id);
			return await result
				.Select(model => new SustainabilityPostEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all SustainabilityPostEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of SustainabilityPostEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<SustainabilityPostEntityDto>> Get(
			[FromQuery]SustainabilityPostEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<SustainabilityPostEntityDto>
			{
				Data = await _crudService.Get<SustainabilityPostEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new SustainabilityPostEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<SustainabilityPostEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create SustainabilityPostEntity
		/// </summary>
		/// <param name="model">The new SustainabilityPostEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The SustainabilityPostEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<SustainabilityPostEntityDto> Post(
			[BindRequired, FromBody] SustainabilityPostEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SustainabilityPostEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create SustainabilityPostEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The SustainabilityPostEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<SustainabilityPostEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<SustainabilityPostEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new SustainabilityPostEntityDto(result);
		}

		/// <summary>
		/// Update an SustainabilityPostEntity
		/// </summary>
		/// <param name="model">The SustainabilityPostEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The SustainabilityPostEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<SustainabilityPostEntityDto> Put(
			[BindRequired, FromBody] SustainabilityPostEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SustainabilityPostEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an SustainabilityPostEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The SustainabilityPostEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<SustainabilityPostEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<SustainabilityPostEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new SustainabilityPostEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a SustainabilityPostEntity
		/// </summary>
		/// <param name="id">The id of the SustainabilityPostEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted SustainabilityPostEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<SustainabilityPostEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Sustainability Posts with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Sustainability Posts</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<SustainabilityPostEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new SustainabilityPostEntityDto(r)),
				"export_sustainability_post.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Sustainability Posts with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Sustainability Posts</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<SustainabilityPostEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new SustainabilityPostEntityDto(r)),
				"export_sustainability_post.csv",
				cancellationToken);
		}


		public class SustainabilityPostEntityOptions : PaginationOptions
		{
		}

	}
}

