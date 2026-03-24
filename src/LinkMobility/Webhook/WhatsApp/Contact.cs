namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a WhatsApp contact.
	/// </summary>
	public class Contact
	{
		/// <summary>
		/// The user profile information.
		/// </summary>
		[JsonProperty("profile")]
		public Profile? Profile { get; set; }

		/// <summary>
		/// WhatsApp user ID.
		/// </summary>
		/// <remarks>
		/// Note that a WhatsApp user's ID and phone number may not always match.
		/// </remarks>
		[JsonProperty("wa_id")]
		public string? WhatsAppId { get; set; }

		/// <summary>
		/// Identity key hash.
		/// </summary>
		/// <remarks>
		/// Only included if you have enabled the <see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/business-phone-numbers/phone-numbers">identity change check</see> feature.
		/// </remarks>
		[JsonProperty("identity_key_hash")]
		public string? IdentityKeyHash { get; set; }
	}
}
