
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lactalis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lactalis.Utility
{
	public class AuditMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<AuditMiddleware> _logger;
		private readonly ILogger<AuditLog> _auditLogger;

		public AuditMiddleware(
			RequestDelegate next,
			ILogger<AuditMiddleware> logger,
			ILogger<AuditLog> auditLogger
			)
		{
			_next = next;
			_logger = logger;
			_auditLogger = auditLogger;
		}

		public async Task InvokeAsync(HttpContext context)
		{

			await _next.Invoke(context);

			try
			{
				foreach (var log in GetLogs(context))
				{
					log.LogAudit(_auditLogger);
				}
			}
			catch (Exception e)
			{
				_logger.LogError(e.ToString());
			}
		}

		private static List<AuditLog> GetLogs(HttpContext context)
		{
			if (!context.Items.ContainsKey("AuditLogs"))
			{
				return new List<AuditLog>();
			}

			if (context.Items["AuditLogs"] is IList item)
			{
				return item.Cast<AuditLog>().ToList();
			}

			return new List<AuditLog>();
		}

	}
}
