namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a notification for an incoming WhatsApp message or delivery report.
	/// (<see href="https://developer.linkmobility.eu/whatsapp-api/receive-whatsapp-messages">API</see>)
	/// </summary>
	public class WhatsAppNotification
	{
		/// <summary>
		/// A unique identifier for the customer channel. It is typically in UUID format.
		/// </summary>
		[JsonProperty("customerChannelUuid")]
		public Guid CustomerChannelUuid { get; set; }

		/// <summary>
		/// The sender's information in E164 formatted MSISDN (see Wikipedia <see href="https://en.wikipedia.org/wiki/MSISDN">MSISDN</see>).
		/// In this case is the customer phone number.
		/// </summary>
		[JsonProperty("sender")]
		public string? Sender { get; set; }

		/// <summary>
		/// The recipient's information in E164 formatted MSISDN (see Wikipedia <see href="https://en.wikipedia.org/wiki/MSISDN">MSISDN</see>).
		/// In this case is the Customer Channel identifier.
		/// </summary>
		[JsonProperty("recipient")]
		public string? Recipient { get; set; }

		/// <summary>
		/// The type of the communication channel.
		/// In this case, it is specified as <c>whatsapp</c>.
		/// </summary>
		[JsonProperty("type")]
		public string? Type { get; set; }

		/// <summary>
		/// Meta WhatsApp Notification payload.
		/// </summary>
		/// <remarks>
		/// See specification on <see href="https://developers.facebook.com/docs/whatsapp/cloud-api/webhooks/components">Meta documentation</see>.
		/// </remarks>
		[JsonProperty("whatsappNotification")]
		public Notification? Body { get; set; }
	}
}
