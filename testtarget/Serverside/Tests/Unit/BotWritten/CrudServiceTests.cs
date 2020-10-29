

using Lactalis.Services;
using Moq;
using Xunit;

namespace ServersideTests.Tests.Unit.BotWritten
{
	public class CrudServiceTests
	{
		[Theory]
		[InlineData(null, null, false)]
		[InlineData(null, 0, false)]
		[InlineData(0, null, false)]
		[InlineData(0, 0, false)]
		[InlineData(1, 1, true)]
		public void TestPaginationValidation(int? pageNum, int? pageSize, bool expectedValidation)
		{
			var paginationOptions = new PaginationOptions
			{
				PageNo = pageNum,
				PageSize = pageSize
			};

			var pagination = new Pagination(paginationOptions);

			Assert.Equal(expectedValidation, pagination.isValid());
		}
	}
}
