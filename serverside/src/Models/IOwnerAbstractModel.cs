
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Lactalis.Security;
 

namespace Lactalis.Models
{
	public interface IOwnerAbstractModel : IAbstractModel
	{
		Guid Owner { get; set; }
		IEnumerable<IAcl> Acls { get; }

		/// <summary>
		/// Deletes all entities of a specified type that are related to the provided entity
		/// </summary>
		/// <param name="reference">The reference to delete entities from</param>
		/// <param name="models">A list of models of the type of the current object which will have their related data deleted</param>
		/// <param name="dbContext">The database context that contains the data</param>
		/// <param name="cancellation">Cancellation token for the operation</param>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		Task<int> CleanReference<T>(
			string reference,
			IEnumerable<T> models,
			LactalisDBContext dbContext,
			CancellationToken cancellation = default)
			where T : IOwnerAbstractModel;

	}
}