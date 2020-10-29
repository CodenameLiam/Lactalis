
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Lactalis.Models
{
	public class ChangeState
	{
		public EntityState State { get; set; }
		public EntityEntry<IAbstractModel> Entry { get; set; }
	}
}