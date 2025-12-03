using AMWD.Net.Api.LinkMobility;

namespace LinkMobility.Tests.Models
{
	[TestClass]
	public class IncomingMessageNotificationTest
	{
		[TestMethod]
		public void ShouldParseAllPropertiesForTextNotification()
		{
			// Arrange
			string json = @"{
				""messageType"": ""text"",
				""notificationId"": ""notif-123"",
				""transferId"": ""trans-456"",
				""messageFlashSms"": true,
				""senderAddress"": ""436991234567"",
				""senderAddressType"": ""international"",
				""recipientAddress"": ""066012345678"",
				""recipientAddressType"": ""national"",
				""textMessageContent"": ""Hello from user"",
				""userDataHeaderPresent"": false,
				""binaryMessageContent"": [""SGVsbG8=""],
				""deliveryReportMessageStatus"": 2,
				""sentOn"": ""2025-12-03T12:34:56Z"",
				""deliveredOn"": ""2025-12-03T12:35:30Z"",
				""deliveredAs"": 1,
				""clientMessageId"": ""client-789""
			}";

			// Act
			bool successful = IncomingMessageNotification.TryParse(json, out var notification);

			// Assert
			Assert.IsTrue(successful, "TryParse should return true for valid json");
			Assert.IsNotNull(notification);

			Assert.AreEqual(IncomingMessageNotification.Type.Text, notification.MessageType);
			Assert.AreEqual("notif-123", notification.NotificationId);
			Assert.AreEqual("trans-456", notification.TransferId);

			Assert.IsTrue(notification.MessageFlashSms.HasValue && notification.MessageFlashSms.Value);
			Assert.AreEqual("436991234567", notification.SenderAddress);
			Assert.IsTrue(notification.SenderAddressType.HasValue);
			Assert.AreEqual(AddressType.International, notification.SenderAddressType.Value);

			Assert.AreEqual("066012345678", notification.RecipientAddress);
			Assert.IsTrue(notification.RecipientAddressType.HasValue);
			Assert.AreEqual(AddressType.National, notification.RecipientAddressType.Value);

			Assert.AreEqual("Hello from user", notification.TextMessageContent);
			Assert.IsTrue(notification.UserDataHeaderPresent.HasValue && !notification.UserDataHeaderPresent.Value);

			Assert.IsNotNull(notification.BinaryMessageContent);
			CollectionAssert.AreEqual(new List<string> { "SGVsbG8=" }, new List<string>(notification.BinaryMessageContent));

			// delivery status and deliveredAs are numeric in the test json: assert underlying integral values
			Assert.IsTrue(notification.DeliveryReportMessageStatus.HasValue);
			Assert.AreEqual(2, (int)notification.DeliveryReportMessageStatus.Value);

			Assert.IsTrue(notification.SentOn.HasValue);
			Assert.IsTrue(notification.DeliveredOn.HasValue);

			// Compare instants in UTC
			var expectedSent = DateTime.Parse("2025-12-03T12:34:56Z").ToUniversalTime();
			var expectedDelivered = DateTime.Parse("2025-12-03T12:35:30Z").ToUniversalTime();
			Assert.AreEqual(expectedSent, notification.SentOn.Value.ToUniversalTime());
			Assert.AreEqual(expectedDelivered, notification.DeliveredOn.Value.ToUniversalTime());

			Assert.IsTrue(notification.DeliveredAs.HasValue);
			Assert.AreEqual(1, (int)notification.DeliveredAs.Value);

			Assert.AreEqual("client-789", notification.ClientMessageId);
		}

		[TestMethod]
		public void TryParseShouldReturnFalseOnInvalidJson()
		{
			// Arrange
			string invalid = "this is not json";

			// Act
			bool successful = IncomingMessageNotification.TryParse(invalid, out var notification);

			// Assert
			Assert.IsFalse(successful);
			Assert.IsNull(notification);
		}
	}
}
