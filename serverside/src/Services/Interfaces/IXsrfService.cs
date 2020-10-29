
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Lactalis.Services.Interfaces
{
	public interface IXsrfService
	{
		/// <summary>
		/// Adds an XSRF token to the HttpContext using the user claim attached to the HttpContext
		/// </summary>
		/// <param name="context">The HttpContext to add the token to</param>
		void AddXsrfToken(HttpContext context);

		/// <summary>
		/// Adds a XSRF token to the HttpContext using the provided user claim
		/// </summary>
		/// <param name="context">The HttpContext to add the token to</param>
		/// <param name="userClaim">The ClaimsPrincipal to generate the XSRF token with</param>
		void AddXsrfToken(HttpContext context, ClaimsPrincipal userClaim);
	}
}