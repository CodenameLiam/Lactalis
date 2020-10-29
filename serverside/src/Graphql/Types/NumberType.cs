
using GraphQL.Types;

namespace Lactalis.Graphql.Types
{
	/// <summary>
	/// An object that contains a number
	/// </summary>
	public class NumberObject
	{
		public int Number { get; set; }
	}

	/// <summary>
	/// The GraphQL object type for the number
	/// </summary>
	public class NumberObjectType : ObjectGraphType<NumberObject>
	{
		public NumberObjectType()
		{
			Field<IntGraphType>(
				"Number",
				resolve: o => o.Source.Number,
				description: "The total number"
			);
		}
	}
}