using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace LinkMobility.Tests.WhatsApp.Contents
{
	[TestClass]
	public class VideoMessageContentTest
	{
		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("Caption")]
		public void ShouldValidateSuccessful(string caption)
		{
			// Arrange
			var content = new VideoMessageContent("https://example.com/video.mp4");
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
		[DataRow("ftp://example.com/video.mp4")]
		[DataRow("www.example.org/video.mp4")]
		public void ShouldValidateNotSuccessful(string url)
		{
			// Arrange
			var content = new VideoMessageContent(url);

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsFalse(isValid);
		}
	}
}
