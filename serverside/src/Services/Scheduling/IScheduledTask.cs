
using System.Threading;
using System.Threading.Tasks;

namespace Lactalis.Services.Scheduling
{
	public interface IScheduledTask
	{
		string Schedule { get; }
		Task ExecuteAsync(CancellationToken cancellationToken);
	}
}
