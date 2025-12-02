using System.Net.Http;
using System.Text;
using AMWD.Net.Api.LinkMobility;

namespace LinkMobility.Tests.Auth
{
	[TestClass]
	public class BasicAuthenticationTest
	{
		[TestMethod]
		public void ShouldAddHeader()
		{
			// Arrange
			string username = "user";
			string password = "pass";
			var auth = new BasicAuthentication(username, password);

			using var httpClient = new HttpClient();

			// Act
			auth.AddHeader(httpClient);

			// Assert
			Assert.IsTrue(httpClient.DefaultRequestHeaders.Contains("Authorization"));

			Assert.AreEqual("Basic", httpClient.DefaultRequestHeaders.Authorization.Scheme);
			Assert.AreEqual(Base64(username, password), httpClient.DefaultRequestHeaders.Authorization.Parameter);
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public void ShouldThrowArgumentNullExceptionForUsername(string username)
		{
			// Arrange

			// Act & Assert
			Assert.ThrowsExactly<ArgumentNullException>(() => new BasicAuthentication(username, "pass"));
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public void ShouldThrowArgumentNullExceptionForPassword(string password)
		{
			// Arrange

			// Act & Assert
			Assert.ThrowsExactly<ArgumentNullException>(() => new BasicAuthentication("user", password));
		}

		private static string Base64(string user, string pass)
		{
			string plainText = $"{user}:{pass}";
			return Convert.ToBase64String(Encoding.ASCII.GetBytes(plainText));
		}
	}
}
