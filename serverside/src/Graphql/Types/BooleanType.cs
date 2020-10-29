
using GraphQL.Types;

namespace Lactalis.Graphql.Types
{
	/// <summary>
	/// An object that contains a number
	/// </summary>
	public class BooleanObject
	{
		public bool Value { get; set; }
	}

	/// <summary>
	/// The GraphQL object type for the number
	/// </summary>
	public class BooleanObjectType : ObjectGraphType<BooleanObject>
	{
		public BooleanObjectType()
		{
			Field<BooleanGraphType>(
				"value",
				resolve: o => o.Source.Value,
				description: "The value of the boolean"
			);
		}
	}
}