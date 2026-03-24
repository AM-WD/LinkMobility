using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility.Webhook.WhatsApp
{
	/// <summary>
	/// Defines the billing/pricing type.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum BillingType
	{
		/// <summary>
		/// Indicates the message is billable.
		/// </summary>
		[EnumMember(Value = "regular")]
		Regular = 1,

		/// <summary>
		/// Indicates the message is free because it was
		/// either a utility template message
		/// or non-template message sent within a customer service window.
		/// </summary>
		[EnumMember(Value = "free_customer_service")]
		FreeCustomerService = 2,

		/// <summary>
		/// Indicates the message is free because
		/// it was sent within an open free entry point window.
		/// </summary>
		[EnumMember(Value = "free_entry_point")]
		FreeEntryPoint = 3,
	}
}
