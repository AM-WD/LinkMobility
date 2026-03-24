using AMWD.Net.Api.LinkMobility.Utils;

namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// The conversation information.
	/// </summary>
	/// <remarks>
	/// <list type="number">
	/// <item>Only included with sent status, and one of either delivered or read status.</item>
	/// <item>Omitted entirely for v24.0+ unless webhook is for a free entry point conversation.</item>
	/// </list>
	/// </remarks>
	public class Conversation
	{
		/// <summary>
		/// Unique identifier for the conversation.
		/// </summary>
		/// <remarks>
		/// <para>
		/// <strong>Version 24.0 and higher:</strong>
		/// <br/>
		/// The <see cref="Conversation"/> object will be omitted entirely,
		/// unless the webhook is for a message sent within an open free entry point window,
		/// in which case the value will be unique per window.
		/// </para>
		/// <para>
		/// <strong>Version 23.0 and lower:</strong>
		/// <br/>
		/// Value will now be set to a unique ID per-message,
		/// unless the webhook is for a message sent with an open free entry point window,
		/// in which case the value will be unique per window.
		/// </para>
		/// </remarks>
		[JsonProperty("id")]
		public string? Id { get; set; }

		/// <summary>
		/// Timestamp indicating when the conversation will expire.
		/// </summary>
		/// <remarks>
		/// The expiration_timestamp property is only included for <see cref="DeliveryStatus.Sent"/> status.
		/// </remarks>
		[JsonProperty("expiration_timestamp")]
		[JsonConverter(typeof(UnixTimestampJsonConverter))]
		public DateTime? ExpirationTimestamp { get; set; }

		/// <summary>
		/// The conversation origin.
		/// </summary>
		[JsonProperty("origin")]
		public ConversationOrigin? Origin { get; set; }
	}

	/// <summary>
	/// The conversation origin.
	/// </summary>
	public class ConversationOrigin
	{
		/// <summary>
		/// The conversation category.
		/// </summary>
		[JsonProperty("type")]
		public BillingCategory? Type { get; set; }
	}
}
