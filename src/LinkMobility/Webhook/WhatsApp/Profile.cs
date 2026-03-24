namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Represents a WhatsApp user profile.
	/// </summary>
	public class Profile
	{
		/// <summary>
		/// WhatsApp user's name as it appears in their profile in the WhatsApp client.
		/// </summary>
		[JsonProperty("name")]
		public string? Name { get; set; }

		/// <summary>
		/// The username.
		/// </summary>
		[JsonProperty("username")]
		public string? Username { get; set; }

		/// <summary>
		/// The country code.
		/// </summary>
		[JsonProperty("country_code")]
		public string? CountryCode { get; set; }
	}
}
