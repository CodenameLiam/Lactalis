
using System.Collections.Generic;
using Lactalis.Models;

namespace Lactalis.Services.Interfaces
{
	public interface IAuditService
	{
		/// <summary>
		/// A list of audit logs for this service scope
		/// </summary>
		List<AuditLog> Logs { get; }

		/// <summary>
		/// Creates a audit log formatted for read operations
		/// </summary>
		/// <param name="userId">The id of the user performing the read operation</param>
		/// <param name="modelName">The name of the entity that is being fetched</param>
		/// <param name="data">Any audit data to be stored in the log</param>
		void CreateReadAudit(string userId, string userName, string modelName, object data = null);

	}
}