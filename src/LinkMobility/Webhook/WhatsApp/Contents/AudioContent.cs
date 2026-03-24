namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a received audio file.
	/// </summary>
	public class AudioContent
	{
		/// <summary>
		/// Media asset MIME type.
		/// </summary>
		[JsonProperty("mime_type")]
		public string? MimeType { get; set; }

		/// <summary>
		/// Media asset SHA-256 hash.
		/// </summary>
		[JsonProperty("sha256")]
		public string? Sha256 { get; set; }

		/// <summary>
		/// Media asset ID.
		/// </summary>
		/// <remarks>
		/// You can <see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/business-phone-numbers/media">perform a GET on this ID</see> to get the asset URL,
		/// then perform a GET on the returned URL (using your access token) to get the underlying asset.
		/// </remarks>
		[JsonProperty("id")]
		public string? Id { get; set; }

		/// <summary>
		/// Media URL.
		/// </summary>
		/// <remarks>
		/// You can query this URL directly with your access token to <see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/business-phone-numbers/media#download-media">download the media asset</see>.
		/// </remarks>
		[JsonProperty("url")]
		public string? Url { get; set; }

		/// <summary>
		/// indicating if audio is a recording made with the WhatsApp client voice recording feature.
		/// </summary>
		[JsonProperty("voice")]
		public bool? IsVoiceRecord { get; set; }
	}
}
