using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace LinkMobility.Tests.WhatsApp.Contents
{
	[TestClass]
	public class AudioMessageContentTest
	{
		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("Caption")]
		public void ShouldValidateSuccessful(string caption)
		{
			// Arrange
			var content = new AudioMessageContent("https://example.com/audio.mp3");
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
		[DataRow("ftp://example.com/audio.mp3")]
		[DataRow("www.example.org/audio.mp3")]
		public void ShouldValidateNotSuccessful(string url)
		{
			// Arrange
			var content = new AudioMessageContent(url);

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsFalse(isValid);
		}
	}
}
