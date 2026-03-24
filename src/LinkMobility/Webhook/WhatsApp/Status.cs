using AMWD.Net.Api.LinkMobility.Utils;

namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a status change of a WhatsApp message.
	/// </summary>
	public class Status
	{
		/// <summary>
		/// WhatsApp message ID.
		/// </summary>
		[JsonProperty("id")]
		public string? Id { get; set; }

		/// <summary>
		/// Message status.
		/// </summary>
		[JsonProperty("status")]
		public DeliveryStatus? DeliveryStatus { get; set; }

		/// <summary>
		/// Timestamp indicating when the webhook was triggered.
		/// </summary>
		[JsonProperty("timestamp")]
		[JsonConverter(typeof(UnixTimestampJsonConverter))]
		public DateTime? Timestamp { get; set; }

		/// <summary>
		/// WhatsApp user phone number or group ID.
		/// </summary>
		/// <remarks>
		/// Value set to the WhatsApp user's phone number if the message was sent to their phone number, or set to a group ID if sent to a group ID.
		/// If sent to a group ID, the WhatsApp user's phone number is instead assigned to the <see cref="RecipientParticipantId"/> property.
		/// </remarks>
		[JsonProperty("recipient_id")]
		public string? RecipientId { get; set; }

		/// <summary>
		/// WhatsApp user phone number. Property only included if message was sent to a group.
		/// </summary>
		[JsonProperty("recipient_participant_id")]
		public string? RecipientParticipantId { get; set; }

		/// <summary>
		/// The conversation information.
		/// </summary>
		/// <remarks>
		/// <list type="number">
		/// <item>Only included with sent status, and one of either delivered or read status.</item>
		/// <item>Omitted entirely for v24.0+ unless webhook is for a free entry point conversation.</item>
		/// </list>
		/// </remarks>
		[JsonProperty("conversation")]
		public Conversation? Conversation { get; set; }

		/// <summary>
		/// The pricing information.
		/// </summary>
		/// <remarks>
		/// Only included with <see cref="DeliveryStatus.Sent"/> status, and one of either <see cref="DeliveryStatus.Delivered"/> or <see cref="DeliveryStatus.Read"/> status.
		/// </remarks>
		[JsonProperty("pricing")]
		public Pricing? Pricing { get; set; }
	}
}
