using System.Net.Http;
using AMWD.Net.Api.LinkMobility;

namespace LinkMobility.Tests.Auth
{
	[TestClass]
	public class AccessTokenAuthenticationTest
	{
		[TestMethod]
		public void ShouldAddHeader()
		{
			// Arrange
			string token = "test_token";
			var auth = new AccessTokenAuthentication(token);

			using var httpClient = new HttpClient();

			// Act
			auth.AddHeader(httpClient);

			// Assert
			Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("Authorization"));

			Assert.AreEqual("Bearer", httpClient.DefaultRequestHeaders.Authorization.Scheme);
			Assert.AreEqual(token, httpClient.DefaultRequestHeaders.Authorization.Parameter);
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public void ShouldThrowArgumentNullExceptionForToken(string token)
		{
			// Arrange

			// Act & Assert
			Assert.ThrowsExactly<ArgumentNullException>(() => new AccessTokenAuthentication(token));
		}
	}
}
