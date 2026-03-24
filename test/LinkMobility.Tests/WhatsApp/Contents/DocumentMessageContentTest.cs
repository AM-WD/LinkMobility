using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace LinkMobility.Tests.WhatsApp.Contents
{
	[TestClass]
	public class DocumentMessageContentTest
	{
		[TestMethod]
		[DataRow(null)]
		[DataRow("")]
		[DataRow("Caption")]
		public void ShouldValidateSuccessful(string caption)
		{
			// Arrange
			var content = new DocumentMessageContent("https://example.com/doc.pdf");
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
		[DataRow("ftp://example.com/doc.pdf")]
		[DataRow("www.example.org/doc.pdf")]
		public void ShouldValidateNotSuccessful(string url)
		{
			// Arrange
			var content = new DocumentMessageContent(url);

			// Act
			bool isValid = content.IsValid();

			// Assert
			Assert.IsFalse(isValid);
		}
	}
}
