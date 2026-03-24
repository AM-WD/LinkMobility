namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// A WhatsApp notification entry.
	/// </summary>
	public class NotificationEntry
	{
		/// <summary>
		/// The WhatsApp business account ID.
		/// </summary>
		[JsonProperty("id")]
		public string? Id { get; set; }

		/// <summary>
		/// Changes of that entry.
		/// </summary>
		[JsonProperty("changes")]
		public IReadOnlyCollection<NotificationChange>? Changes { get; set; }
	}
}
