namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// A value of the notification change.
	/// </summary>
	public class NotificationValue
	{
		/// <summary>
		/// The type of messaging product that triggered the webhook.
		/// Will be <c>whatsapp</c>.
		/// </summary>
		[JsonProperty("messaging_product")]
		public string? MessagingProduct { get; set; }

		/// <summary>
		/// Metadata about the notification change.
		/// </summary>
		[JsonProperty("metadata")]
		public NotificationMetadata? Metadata { get; set; }

		/// <summary>
		/// Contacts of the WhatsApp users involved in the notification change.
		/// </summary>
		[JsonProperty("contacts")]
		public IReadOnlyCollection<Contact>? Contacts { get; set; }

		/// <summary>
		/// The messages involved in the notification change.
		/// </summary>
		/// <remarks>
		/// LINK Mobility API docs: <see href="https://developer.linkmobility.eu/whatsapp-api/rest-api#/paths/receiveIncomingWhatsappmessaging/post"/>
		/// </remarks>
		[JsonProperty("messages")]
		public IReadOnlyCollection<Message>? Messages { get; set; }

		/// <summary>
		/// Status changes of the messages involved in the notification change.
		/// </summary>
		/// <remarks>
		/// LINK Mobility API docs: <see href="https://developer.linkmobility.eu/whatsapp-api/rest-api#/paths/receiveStatusWhatsappmessaging/post"/>
		/// </remarks>
		[JsonProperty("statuses")]
		public IReadOnlyCollection<Status>? Statuses { get; set; }
	}
}
