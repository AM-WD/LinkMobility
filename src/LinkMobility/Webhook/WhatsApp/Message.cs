using AMWD.Net.Api.LinkMobility.Utils;
using AMWD.Net.Api.LinkMobility.WhatsApp;

namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a received WhatsApp message.
	/// </summary>
	public class Message
	{
		/// <summary>
		/// WhatsApp user phone number.
		/// </summary>
		/// <remarks>
		/// This is the same value returned by the API as the input value when sending a message to a WhatsApp user.
		/// Note that a WhatsApp user's phone number and ID may not always match.
		/// </remarks>
		[JsonProperty("from")]
		public string? From { get; set; }

		/// <summary>
		/// WhatsApp message ID.
		/// </summary>
		[JsonProperty("id")]
		public string? Id { get; set; }

		/// <summary>
		/// Unix timestamp indicating when the webhook was triggered.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(UnixTimestampJsonConverter))]
		public DateTime? Timestamp { get; set; }

		/// <summary>
		/// The type of message received.
		/// </summary>
		[JsonProperty("type")]
		public MessageType? Type { get; set; }

		#region Content depending on the message type

		/// <summary>
		/// Audio file content.
		/// </summary>
		[JsonProperty("audio")]
		public AudioContent? Audio { get; set; }

		/// <summary>
		/// Document file content.
		/// </summary>
		[JsonProperty("document")]
		public DocumentContent? Document { get; set; }

		/// <summary>
		/// Image file content.
		/// </summary>
		[JsonProperty("image")]
		public ImageContent? Image { get; set; }

		/// <summary>
		/// Content of a text message.
		/// </summary>
		[JsonProperty("text")]
		public TextContent? Text { get; set; }

		/// <summary>
		/// Video file content.
		/// </summary>
		[JsonProperty("video")]
		public VideoContent? Video { get; set; }

		#endregion Content depending on the message type
	}
}
