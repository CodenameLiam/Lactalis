
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
	/// The controller that provides rest endpoints for the AdminEntity model
	/// </summary>
	[Route("/api/entity/AdminEntity")]
	[Authorize]
	[ApiController]
	public class AdminEntityController : BaseApiController
	{
		private readonly ICrudService _crudService;

		public AdminEntityController(
			ICrudService crudService)
		{
			_crudService = crudService;
		}

		/// <summary>
		/// Get the AdminEntity for the given id
		/// </summary>
		/// <param name="id">The id of the AdminEntity to be fetched</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>The AdminEntity object with the given id</returns>
		[HttpGet]
		[Route("{id}")]
		[Authorize]
		public async Task<AdminEntityDto> Get(Guid id, CancellationToken cancellation)
		{
			var result = _crudService.GetById<AdminEntity>(id);
			return await result
				.Select(model => new AdminEntityDto(model))
				.AsNoTracking()
				.FirstOrDefaultAsync(cancellation);
		}

		/// <summary>
		/// Gets all AdminEntitys with pagination support
		/// </summary>
		/// <param name="options">Filtering params</param>
		/// <param name="cancellation">A cancellation token</param>
		/// <returns>A list of AdminEntitys</returns>
		[HttpGet]
		[Route("")]
		[Authorize]
		public async Task<EntityControllerData<AdminEntityDto>> Get(
			[FromQuery]AdminEntityOptions options,
			CancellationToken cancellation)
		{
			
			return new EntityControllerData<AdminEntityDto>
			{
				Data = await _crudService.Get<AdminEntity>(new Pagination(options))
					.AsNoTracking()
					.Select(model => new AdminEntityDto(model))
					.ToListAsync(cancellation),
				Count = await _crudService.Get<AdminEntity>()
					.AsNoTracking()
					.CountAsync(cancellation)
			};
		}

		/// <summary>
		/// Create AdminEntity
		/// </summary>
		/// <param name="model">The new AdminEntity to be created</param>
		/// <param name="cancellation">The cancellation token for this operation</param>
		/// <returns>The AdminEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("application/json")]
		[Authorize]
		public async Task<AdminEntityDto> Post(
			[BindRequired, FromBody] AdminEntityDto model,
			CancellationToken cancellation)
		{
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AdminEntityDto(await _crudService.Create(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Create AdminEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AdminEntity object after creation</returns>
		[HttpPost]
		[Route("")]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<AdminEntityDto> PostForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<AdminEntityDto>(variables.First());
			
			if (model.Id != Guid.Empty)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			var result = await _crudService.Create(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation);

			return new AdminEntityDto(result);
		}

		/// <summary>
		/// Update an AdminEntity
		/// </summary>
		/// <param name="model">The AdminEntity to be updated</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AdminEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("application/json")]
		[Authorize]
		public async Task<AdminEntityDto> Put(
			[BindRequired, FromBody] AdminEntityDto model,
			CancellationToken cancellation)
		{
			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AdminEntityDto(await _crudService.Update(model.ToModel(), cancellation: cancellation));
		}

		/// <summary>
		/// Update an AdminEntity
		/// </summary>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The AdminEntity object after it has been updated</returns>
		[HttpPut]
		[Consumes("multipart/form-data")]
		[Authorize]
		public async Task<AdminEntityDto> PutForm(CancellationToken cancellation)
		{
			var form = await Request.ReadFormAsync(cancellation);
			form.TryGetValue("variables", out var variables);
			var model = JsonConvert.DeserializeObject<AdminEntityDto>(variables.First());

			if (Guid.Empty == model.Id)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return null;
			}

			return new AdminEntityDto(await _crudService.Update(model.ToModel(), new UpdateOptions
			{
				Files = form.Files,
			}, cancellation));
		}

		/// <summary>
		/// Deletes a AdminEntity
		/// </summary>
		/// <param name="id">The id of the AdminEntity to delete</param>
		/// <param name="cancellation">The cancellation token</param>
		/// <returns>The ids of the deleted AdminEntitys</returns>
		[HttpDelete]
		[Route("{id}")]
		[Authorize]
		public async Task<Guid> Delete(Guid id, CancellationToken cancellation)
		{
			return await _crudService.Delete<AdminEntity>(id, cancellation);
		}

		/// <summary>
		/// Export the list of Admins with given the provided conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Admins</returns>
		[HttpGet]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task Export(
			[FromQuery]IEnumerable<WhereExpression> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<AdminEntity>()
				.AsNoTracking()
				.AddWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new AdminEntityDto(r)),
				"export_admin.csv",
				cancellationToken);
		}

		/// <summary>
		/// Export a list of Admins with given the provided conditions
		/// This is a post endpoint for easier composition of complex conditions
		/// </summary>
		/// <param name="conditions">The conditions to export with</param>
		/// <param name="cancellationToken">The cancellation token for the request</param>
		/// <returns>A csv file containing the export of Admins</returns>
		[HttpPost]
		[Route("export")]
		[Produces("text/csv")]
		[Authorize]
		public async Task ExportPost(
			[FromBody]IEnumerable<IEnumerable<WhereExpression>> conditions,
			CancellationToken cancellationToken)
		{
			var queryable = _crudService.Get<AdminEntity>()
				.AsNoTracking()
				.AddConditionalWhereFilter(conditions);

			await WriteQueryableCsvAsync(
				queryable.Select(r => new AdminEntityDto(r)),
				"export_admin.csv",
				cancellationToken);
		}


		public class AdminEntityOptions : PaginationOptions
		{
		}

	}
}

