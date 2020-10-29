
using System;
using System.Linq;
using Audit.Core;
using Audit.EntityFramework;
using Lactalis.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Lactalis.Utility
{
	public static class AuditUtilities
	{
		public static void LogAuditEvent(AuditEvent audit, ILogger<AuditLog> logger)
		{
			var efEvent = audit.GetEntityFrameworkEvent();
			var dbContext = efEvent.GetDbContext() as LactalisDBContext;
			foreach (var entry in efEvent.Entries)
			{
				var entity = new AuditLog
				{
					Id = Guid.NewGuid(),
					AuditData = JObject.FromObject(new
					{
						Table = entry.Table,
						Action = entry.Action,
						PrimaryKey = entry.PrimaryKey,
						ColumnValues = entry.ColumnValues,
						Values = entry
							.Changes
							?.Where(e => e.NewValue != null)
							.Select(e => new {ColumnName = e.ColumnName, Value = e.NewValue})
							.ToList()
					}),
					EntityType = entry.Table + "Entity",
					AuditDate = DateTime.UtcNow,
					Action = (entry.Action == "Insert") ? "Create" : entry.Action,
					TablePk = entry.PrimaryKey.First().Value.ToString(),
					UserId = dbContext?.SessionUserId,
					UserName = dbContext?.SessionUser,
					HttpContextId = dbContext?.SessionId
				};

				entity.LogAudit(logger);
			}
		}
	}
}