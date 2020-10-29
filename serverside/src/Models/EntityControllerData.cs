
using System.Collections.Generic;

namespace Lactalis.Models
{
	public class EntityControllerData<T>
	{
		public IEnumerable<T> Data { get; set; }
		public int Count { get; set; }
	}
}