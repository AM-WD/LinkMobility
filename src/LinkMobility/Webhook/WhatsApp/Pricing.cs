namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// The pricing information for a WhatsApp message.
	/// </summary>
	public class Pricing
	{
		/// <summary>
		/// Indicates if the message is billable (<see langword="true"/>) or not (<see langword="false"/>).
		/// </summary>
		/// <remarks>
		/// Note that the <see cref="Billable"/> property will be deprecated in a future versioned release,
		/// so we recommend that you start using <see cref="Type"/> and <see cref="Category"/> together to determine if a message is billable,
		/// and if so, its billing rate.
		/// </remarks>
		[JsonProperty("billable")]
		public bool? Billable { get; set; }

		/// <summary>
		/// Pricing model.
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item>
		/// <c>CBP</c>:
		/// Indicates conversation-based pricing applies.
		/// Will only be set to this value if the webhook was sent before <em>2025-07-01</em>.
		/// </item>
		/// <item>
		/// <c>PMP</c>:
		/// Indicates <see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/pricing">per-message pricing</see> applies.
		/// </item>
		/// </list>
		/// </remarks>
		[JsonProperty("pricing_model")]
		public string? PricingModel { get; set; }

		/// <summary>
		/// Pricing type.
		/// </summary>
		[JsonProperty("type")]
		public BillingType? Type { get; set; }

		/// <summary>
		/// Pricing category (rate) applied if billable.
		/// </summary>
		[JsonProperty("category")]
		public BillingCategory? Category { get; set; }
	}
}
