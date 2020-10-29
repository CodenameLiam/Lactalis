
using System.Collections.Generic;

namespace APITests.Utils
{
	public class EqualityListComparer<T> : EqualityComparer<T>
	{
		public override bool Equals(T x, T y) => x.Equals(y);
		public override int GetHashCode(T obj) => obj.GetHashCode();
	}
}