using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace LinkMobility.Tests.WhatsApp.Contents
{
	[TestClass]
	public class TextMessageContentTest
	{
		[TestMethod]
		[DataRow(true)]
		[DataRow(false)]
		public void ShouldValidateSuccessful(bool previewUrl)
		{
			// Arrange
			var content = new TextMessageContent("Hello, World!");
			content.Body.PreviewUrl = previewUrl;

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsTrue(isValid);
		}

		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("   ")]
		public void ShouldValidateNotSuccessful(string text)
		{
			// Arrange
			var content = new TextMessageContent(text);

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsFalse(isValid);
		}
	}
}
