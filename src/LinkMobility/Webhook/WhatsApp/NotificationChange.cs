namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// A change of a WhatsApp notification entry.
	/// </summary>
	public class NotificationChange
	{
		/// <summary>
		/// The field category.
		/// </summary>
		[JsonProperty("field")]
		public string? Field { get; set; }

		/// <summary>
		/// The change value.
		/// </summary>
		[JsonProperty("value")]
		public NotificationValue? Value { get; set; }
	}
}
