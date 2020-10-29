
using System.Collections.Generic;
using System.Threading.Tasks;
using Lactalis.Models;

namespace Lactalis.Services.Interfaces
{
	public interface ISecurityService
	{
		/// <summary>
		/// Checks weather all the changes in the change tracker of the scoped db context are abiding the security rules
		/// </summary>
		/// <param name="identityService">The identity service to fetch the user from</param>
		/// <param name="dbContext">The DbContext to check the changes against</param>
		/// <returns>A List of security exceptions as strings for each violation of the security rules</returns>
		Task<List<string>> CheckDbSecurityChanges(IIdentityService identityService, LactalisDBContext dbContext);
	}
}