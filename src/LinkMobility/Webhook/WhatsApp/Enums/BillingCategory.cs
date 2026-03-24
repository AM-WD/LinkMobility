using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Defines the available pricing category (<see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/pricing#rates">rates</see>).
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum BillingCategory
	{
		/// <summary>
		/// Indicates an authentication conversation.
		/// </summary>
		[EnumMember(Value = "authentication")]
		Authentication = 1,

		/// <summary>
		/// Indicates an <see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/pricing/authentication-international-rates">authentication-international</see> conversation.
		/// </summary>
		[EnumMember(Value = "authentication_international")]
		AuthenticationInternational = 2,

		/// <summary>
		/// Indicates a <see href="https://developers.facebook.com/documentation/business-messaging/whatsapp/marketing-messages/overview">marketing</see> conversation.
		/// </summary>
		[EnumMember(Value = "marketing")]
		Marketing = 3,

		/// <summary>
		/// Indicates a <see href="https://developers.facebook.com/docs/whatsapp/marketing-messages-lite-api">Marketing Messages Lite API</see> conversation.
		/// </summary>
		[EnumMember(Value = "marketing_lite")]
		MarketingLite = 4,

		/// <summary>
		/// Indicates a free entry point conversation.
		/// </summary>
		[EnumMember(Value = "referral_conversion")]
		ReferralConversion = 5,

		/// <summary>
		/// Indicates a service conversation.
		/// </summary>
		[EnumMember(Value = "service")]
		Service = 6,

		/// <summary>
		/// Indicates a utility conversation.
		/// </summary>
		[EnumMember(Value = "utility")]
		Utility = 7,
	}
}
