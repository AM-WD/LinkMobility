namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a received text.
	/// </summary>
	public class TextContent
	{
		/// <summary>
		/// The text content of the message.
		/// </summary>
		[JsonProperty("body")]
		public string? Body { get; set; }
	}
}
