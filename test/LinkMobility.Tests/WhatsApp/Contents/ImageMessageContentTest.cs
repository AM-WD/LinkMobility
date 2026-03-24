using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace LinkMobility.Tests.WhatsApp.Contents
{
	[TestClass]
	public class ImageMessageContentTest
	{
		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("Caption")]
		public void ShouldValidateSuccessful(string caption)
		{
			// Arrange
			var content = new ImageMessageContent("https://example.com/image.jpg");
			content.Body.Caption = caption;

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsTrue(isValid);
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		[DataRow("ftp://example.com/image.jpg")]
		[DataRow("www.example.org/image.jpg")]
		public void ShouldValidateNotSuccessful(string url)
		{
			// Arrange
			var content = new ImageMessageContent(url);

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsFalse(isValid);
		}
	}
}
