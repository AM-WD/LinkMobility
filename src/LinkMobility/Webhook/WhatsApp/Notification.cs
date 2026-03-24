namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a Meta WhatsApp Notification payload.
	/// </summary>
	public class Notification
	{
		/// <summary>
		/// The object.
		/// In this case, it is specified as <c>whatsapp_business_account</c>.
		/// </summary>
		[JsonProperty("object")]
		public string? Object { get; set; }

		/// <summary>
		/// Entries of the notification object.
		/// </summary>
		[JsonProperty("entry")]
		public IReadOnlyCollection<NotificationEntry>? Entries { get; set; }
	}
}
