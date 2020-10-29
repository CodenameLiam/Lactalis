
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Lactalis.Services.Scheduling
{
	public static class SchedulerExtensions
	{
		public static IServiceCollection AddScheduler(this IServiceCollection services)
		{
			return services.AddScoped<IHostedService, SchedulerHostedService>();
		}

		public static IServiceCollection AddScheduler(this IServiceCollection services, EventHandler<UnobservedTaskExceptionEventArgs> unobservedTaskExceptionHandler)
		{
			return services.AddScoped<IHostedService, SchedulerHostedService>(serviceProvider =>
			{
				var instance = new SchedulerHostedService(serviceProvider.GetServices<IScheduledTask>(), serviceProvider);
				instance.UnobservedTaskException += unobservedTaskExceptionHandler;
				return instance;
			});
		}
	}
}