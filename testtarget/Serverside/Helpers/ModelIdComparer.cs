
using System.Collections.Generic;
using Lactalis.Models;

namespace ServersideTests.Helpers
{
	/// <summary>
	/// A comparer for to abstract models that compares only if the ids are the same
	/// </summary>
	public class ModelIdComparer : IEqualityComparer<IAbstractModel>
	{
		public bool Equals(IAbstractModel x, IAbstractModel y)
		{
			return (x, y) switch
			{
				(null, null) => true,
				(_, null) => false,
				(null, _) => false,
				var (first, second) => first.Id == second.Id,
			};
		}

		public int GetHashCode(IAbstractModel obj)
		{
			return obj.GetHashCode();
		}
	}
}