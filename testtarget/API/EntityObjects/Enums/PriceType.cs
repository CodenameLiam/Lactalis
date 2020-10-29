

using AutoFixture;
using TestDataLib;

namespace EntityObject.Enums
{
	public enum PriceType
	{
		AMOUNT,
		NEGOTIABLE,
		FREE,
		SWAPTRADE,
	}

	internal static class PriceTypeEnum
	{
		public static PriceType GetRandomPriceType() => new Fixture().Create<PriceType>();

		public static string GetInvalidPriceType()
		{
			return DataUtils.RandString(charType: CharType.FIXTURE_STRING);
		}
	}
}