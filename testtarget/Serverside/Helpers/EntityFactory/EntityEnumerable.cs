
using System.Collections.Generic;
using Lactalis.Models;

namespace ServersideTests.Helpers.EntityFactory
{
	/// <summary>
	/// A list of tracked entities
	/// </summary>
	/// <typeparam name="T">The base entity type that is tracked</typeparam>
	public class EntityEnumerable<T> : List<T>
		where T : class, IAbstractModel
	{
		/// <summary>
		/// A set of all tracked entities
		/// </summary>
		public HashSet<IAbstractModel> AllEntities { get; } = new HashSet<IAbstractModel>();

		/// <summary>
		/// Construct a new entity collection
		/// </summary>
		public EntityEnumerable()
		{

		}

		/// <summary>
		/// Construct a new entity collection with the given elements
		/// </summary>
		/// <param name="enumerable"></param>
		public EntityEnumerable(IEnumerable<T> enumerable)
		{
			foreach (var entity in enumerable)
			{
				Add(entity);
				AllEntities.Add(entity);
			}
		}
	}
}