
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lactalis.Utility
{
	public class AntiforgeryFilterAttribute : Attribute, IFilterFactory, IOrderedFilter
	{
		public bool IsReusable => true;
		public int Order { get; set; } = 1000;

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			return serviceProvider.GetRequiredService<AntiforgeryFilter>();
		}
	}

	public class AntiforgeryFilter : IAsyncAuthorizationFilter, IAntiforgeryPolicy
	{
		private readonly IAntiforgery _antiforgery;
		private readonly ILogger<AntiforgeryFilter> _logger;

		public AntiforgeryFilter(IAntiforgery antiforgery, ILogger<AntiforgeryFilter> logger)
		{
			_antiforgery = antiforgery;
			_logger = logger;
		}

		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (!context.IsEffectivePolicy<IAntiforgeryPolicy>(this))
			{
				_logger.LogInformation("Global antiforgery filter not most effective filter");
				return;
			}

			if (ShouldValidate(context))
			{
				try
				{
					await _antiforgery.ValidateRequestAsync(context.HttpContext);
				}
				catch (AntiforgeryValidationException exception)
				{
					_logger.LogInformation("Invalid antiforgery request {Message}", exception.Message, exception);
					context.Result = new AntiforgeryValidationFailedResult();
				}
			}
		}

		ed virtual bool ShouldValidate(AuthorizationFilterContext context)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			var method = context.HttpContext.Request.Method;
			const string cookieName = ".AspNetCore." + CookieAuthenticationDefaults.AuthenticationScheme;

			return context.HttpContext.Request.Cookies.ContainsKey(cookieName) &&
				context.HttpContext.User.Identity.IsAuthenticated &&
				!string.Equals("GET", method, StringComparison.OrdinalIgnoreCase) &&
				!string.Equals("HEAD", method, StringComparison.OrdinalIgnoreCase) &&
				!string.Equals("TRACE", method, StringComparison.OrdinalIgnoreCase) &&
				!string.Equals("OPTIONS", method, StringComparison.OrdinalIgnoreCase);
		}
	}
}