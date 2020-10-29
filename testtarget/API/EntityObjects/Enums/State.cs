

using AutoFixture;
using TestDataLib;

namespace EntityObject.Enums
{
	public enum State
	{
		QLD,
		NSW,
		VIC,
		WA,
		SA,
		TAS,
		NT,
	}

	internal static class StateEnum
	{
		public static State GetRandomState() => new Fixture().Create<State>();

		public static string GetInvalidState()
		{
			return DataUtils.RandString(charType: CharType.FIXTURE_STRING);
		}
	}
}