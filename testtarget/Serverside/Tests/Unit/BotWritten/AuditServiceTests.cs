
using System;
using Lactalis.Services;
using Xunit;

namespace ServersideTests.Tests.Unit.BotWritten
{
	[Trait("Category", "BotWritten")]
	[Trait("Category", "Unit")]
	public class AuditServicesTests
	{
		[Fact]
		public void AuditLogsTest()
		{
			var userId = Guid.NewGuid().ToString();
			const string modelName = "TestModel";

			var service = new AuditService(null);
			service.CreateReadAudit(userId, "TestUser", modelName, null);

			Assert.Contains(
				service.Logs,
				log => log.UserId == userId && log.EntityType == modelName);
		}
	}
}