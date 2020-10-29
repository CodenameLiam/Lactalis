
using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.Types;

namespace Lactalis.Graphql.Types
{
	/// <summary>
	/// An object that contains an id for returning with a delete query
	/// </summary>
	public class IdObject
	{
		public Guid Id { get; set; }

		public static List<IdObject> FromList(IEnumerable<Guid> ids)
		{
			return ids.Select(o => new IdObject {Id = o}).ToList();
		}
	}

	/// <summary>
	/// GraphQL object type for returning a list of ids
	/// </summary>
	public class IdObjectType : ObjectGraphType<IdObject>
	{
		public IdObjectType()
		{
			Field<IdGraphType>(
				"Id",
				resolve: o => o.Source.Id,
				description: "An ID in the form of a GUID"
			);
		}
	}
}