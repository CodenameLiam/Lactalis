
using System;
using Audit.EntityFramework;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lactalis.Models
{
	/// <summary>
	/// A log of all operations that have occured on entities in the database
	/// </summary>
	[AuditIgnore]
	public class AuditLog
	{
		/// <summary>
		/// The id of the audit
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// The user that performed the operation
		/// </summary>
		public string UserId { get; set; }

		/// <summary>
		/// The username of the the user that performed the operation
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// The type of entity that the operation has occured on
		/// </summary>
		public string EntityType { get; set; }

		/// <summary>
		/// The action that has occured on the entity
		/// </summary>
		public string Action { get; set; }

		/// <summary>
		/// The primary key of the entity that has had the operation performed on it
		/// </summary>
		public string TablePk { get; set; }

		/// <summary>
		/// The date and time of the audit in UTC
		/// </summary>
		public DateTime AuditDate { get; set; }

		/// <summary>
		/// The audit data stored as a JSON object
		/// </summary>
		public JObject AuditData { get; set; }

		/// <summary>
		/// The trace identifier of the http context if the log occured in a http request
		/// </summary>
		public string HttpContextId { get; set; }

		/// <summary>
		/// Logs the audit to the logger
		/// </summary>
		/// <param name="logger">The logger to log to</param>
		/// <param name="logLevel">The level to log as, defaults to information</param>
		public void LogAudit(ILogger<AuditLog> logger, LogLevel logLevel = LogLevel.Information)
		{
			logger.Log(
				logLevel,
				"Id: {Id} UserId: {UserId} UserName: {UserName} EntityType: {EntityType} Action: {Action} " +
				"TablePk: {TablePk} AuditDate: {AuditDate} AuditData: {AuditData} HttpContextId: {HttpContextId}",
				Id,
				UserId,
				UserName,
				EntityType,
				Action,
				TablePk,
				AuditDate,
				AuditData?.ToString(),
				HttpContextId);
		}

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}