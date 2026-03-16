using AMWD.Net.Api.LinkMobility.Utils;

namespace LinkMobility.Tests.Utils
{
	[TestClass]
	public class ValidationTest
	{
		[TestMethod]
		[DataRow("10000000")]
		[DataRow("12345678")]
		[DataRow("123456789012345")]
		[DataRow("14155552671")]
		public void ShouldValidateMSISDNSuccessful(string msisdn)
		{
			Assert.IsTrue(Validation.IsValidMSISDN(msisdn));
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow(" ")]
		[DataRow("012345678")]
		[DataRow("+123456789")]
		[DataRow("1234 5678")]
		[DataRow("1234567")]
		[DataRow("1234567890123456")]
		[DataRow("abc1234567")]
		public void ShouldValidateMSISDNNotSuccessful(string msisdn)
		{
			Assert.IsFalse(Validation.IsValidMSISDN(msisdn));
		}
	}
}
