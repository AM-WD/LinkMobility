namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents metadata for a notification.
	/// </summary>
	public class NotificationMetadata
	{
		/// <summary>
		/// Business display phone number.
		/// </summary>
		[JsonProperty("display_phone_number")]
		public string? DisplayPhoneNumber { get; set; }

		/// <summary>
		/// Business phone number ID.
		/// </summary>
		[JsonProperty("phone_number_id")]
		public string? PhoneNumberId { get; set; }
	}
}
