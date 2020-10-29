
using System;
using Lactalis.Services;
using Lactalis.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Lactalis.Utility
{
	public class XsrfActionFilterAttribute : Attribute, IFilterFactory, IOrderedFilter
	{
		public bool IsReusable => true;
		public int Order { get; set; } = 1100;

		public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
		{
			return serviceProvider.GetRequiredService<XsrfActionFilter>();
		}
	}

	public class XsrfActionFilter : IActionFilter
	{
		private readonly IXsrfService _xsrfService;

		public XsrfActionFilter(IXsrfService xsrfService)
		{
			_xsrfService = xsrfService;
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			_xsrfService.AddXsrfToken(context.HttpContext);
		}

		public void OnActionExecuted(ActionExecutedContext context)
		{
		}
	}
}