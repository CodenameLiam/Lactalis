
using GraphQL.Types;

namespace Lactalis.Graphql.Types
{
	/// <summary>
	/// An object that contains an ordering condition
	/// </summary>
	public class OrderBy
	{
		public string Path { get; set; }
		public bool? Descending { get; set; }
	}

	/// <summary>
	/// The graphql type for the ordering condition
	/// </summary>
	public class OrderGraph : InputObjectGraphType<OrderBy>
	{
		public OrderGraph()
		{
			Field(x => x.Path)
				.Description("The field to order by");
			Field(x => x.Descending,true)
				.Description("Weather or not the field is descending");
		}
	}
}