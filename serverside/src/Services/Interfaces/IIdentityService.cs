
using System.Collections.Generic;
using System.Threading.Tasks;
using Lactalis.Models;


namespace Lactalis.Services.Interfaces
{
	public interface IIdentityService
	{
		/// <summary>
		/// To set and get the Fetched flag
		/// </summary>
		bool Fetched { get; set; }

		/// <summary>
		/// The groups that the user is in
		/// </summary>
		IList<string> Groups { get; set; }
		
		/// <summary>
		/// The user is performing actions in the scope
		/// </summary>
		User User { get; set; }

		/// <summary>
		/// Retrieves the user from the database if they have not already been fetched. The user to retrieve is taken
		/// from the http context of the scope. If this function has already been called in the scope then it will be a
		/// no op on future calls/
		/// </summary>
		/// <returns>A task that resolves when the user and groups are fetched</returns>
		Task RetrieveUserAsync();

	}
}