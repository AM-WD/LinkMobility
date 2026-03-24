using System.Linq;
using AMWD.Net.Api.LinkMobility.Webhook.WhatsApp;
using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace LinkMobility.Tests.Webhook.WhatsApp
{
	[TestClass]
	public class WhatsAppNotificationTest
	{
		[TestMethod]
		public void ShouldDeserializeWhatsAppNotificationWithMessageAndStatus()
		{
			// Arrange
			string json = @"{
				""customerChannelUuid"": ""11111111-2222-3333-4444-555555555555"",
				""sender"": ""46701234567"",
				""recipient"": ""123e4567-e89b-12d3-a456-426614174000"",
				""type"": ""whatsapp"",
				""whatsappNotification"": {
					""object"": ""whatsapp_business_account"",
					""entry"": [
						{
							""id"": ""123456789"",
							""changes"": [
								{
									""field"": ""messages"",
									""value"": {
										""messaging_product"": ""whatsapp"",
										""metadata"": {
											""display_phone_number"": ""+46701234567"",
											""phone_number_id"": ""111222333""
										},
										""contacts"": [
											{
												""profile"": {
													""name"": ""John Doe""
												},
												""wa_id"": ""46701234567""
											}
										],
										""messages"": [
											{
												""from"": ""46701234567"",
												""id"": ""wamid.123"",
												""timestamp"": 1672531200,
												""type"": ""text"",
												""text"": {
													""body"": ""Hello world""
												}
											}
										],
										""statuses"": [
											{
												""id"": ""wamid.123"",
												""status"": ""delivered"",
												""timestamp"": 1672531200,
												""recipient_id"": ""16505551234"",
												""recipient_participant_id"": ""16505550000"",
												""conversation"": {
													""id"": ""conv-1"",
													""expiration_timestamp"": 1672617600,
													""origin"": {
														""type"": ""service""
													}
												},
												""pricing"": {
													""billable"": true,
													""pricing_model"": ""PMP"",
													""type"": ""regular"",
													""category"": ""service""
												}
											}
										]
									}
								}
							]
						}
					]
				}
			}";

			// Act
			var notification = JsonConvert.DeserializeObject<WhatsAppNotification>(json);

			// Assert
			Assert.IsNotNull(notification);
			Assert.AreEqual(Guid.Parse("11111111-2222-3333-4444-555555555555"), notification.CustomerChannelUuid);
			Assert.AreEqual("46701234567", notification.Sender);
			Assert.AreEqual("123e4567-e89b-12d3-a456-426614174000", notification.Recipient);
			Assert.AreEqual("whatsapp", notification.Type);

			Assert.IsNotNull(notification.Body);
			Assert.AreEqual("whatsapp_business_account", notification.Body.Object);
			Assert.IsNotNull(notification.Body.Entries);
			Assert.HasCount(1, notification.Body.Entries);

			var entry = notification.Body.Entries.First();
			Assert.AreEqual("123456789", entry.Id);
			Assert.IsNotNull(entry.Changes);
			Assert.HasCount(1, entry.Changes);

			var change = entry.Changes.First();
			Assert.AreEqual("messages", change.Field);
			Assert.IsNotNull(change.Value);
			Assert.AreEqual("whatsapp", change.Value.MessagingProduct);

			Assert.IsNotNull(change.Value.Metadata);
			Assert.AreEqual("+46701234567", change.Value.Metadata.DisplayPhoneNumber);
			Assert.AreEqual("111222333", change.Value.Metadata.PhoneNumberId);

			Assert.IsNotNull(change.Value.Contacts);
			Assert.HasCount(1, change.Value.Contacts);

			var contact = change.Value.Contacts.First();
			Assert.IsNotNull(contact.Profile);
			Assert.AreEqual("John Doe", contact.Profile.Name);
			Assert.AreEqual("46701234567", contact.WhatsAppId);

			Assert.IsNotNull(change.Value.Messages);
			Assert.HasCount(1, change.Value.Messages);

			var message = change.Value.Messages.First();
			Assert.AreEqual("46701234567", message.From);
			Assert.AreEqual("wamid.123", message.Id);
			Assert.IsNotNull(message.Timestamp);

			// 1672531200 -> 2023-01-01T00:00:00Z
			var expected = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			Assert.AreEqual(expected, message.Timestamp.Value.ToUniversalTime());
			Assert.IsTrue(message.Type.HasValue);
			Assert.AreEqual(MessageType.Text, message.Type.Value);
			Assert.IsNotNull(message.Text);
			Assert.IsNotNull(message.Text.Body);
			Assert.AreEqual("Hello world", message.Text.Body);

			Assert.IsNotNull(change.Value.Statuses);
			Assert.HasCount(1, change.Value.Statuses);

			var status = change.Value.Statuses.First();
			Assert.AreEqual("wamid.123", status.Id);
			Assert.IsTrue(status.DeliveryStatus.HasValue);
			Assert.AreEqual(DeliveryStatus.Delivered, status.DeliveryStatus.Value);
		}

		[TestMethod]
		public void DeserializeShouldThrowOnInvalidJson()
		{
			// Arrange
			string invalid = "this is not json";

			// Act & Assert
			Assert.ThrowsExactly<JsonReaderException>(() => JsonConvert.DeserializeObject<WhatsAppNotification>(invalid));
		}
	}
}
