using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Defines the delivery status of a message on a report.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum DeliveryStatus
	{
		/// <summary>
		/// Message has been delivered to the recipient.
		/// </summary>
		[EnumMember(Value = "delivered")]
		Delivered = 1,

		/// <summary>
		/// Message not delivered and will be re-tried.
		/// </summary>
		[EnumMember(Value = "undelivered")]
		Undelivered = 2,

		/// <summary>
		/// Message has expired and will no longer re-tried.
		/// </summary>
		[EnumMember(Value = "expired")]
		Expired = 3,

		/// <summary>
		/// Message has been deleted.
		/// </summary>
		[EnumMember(Value = "deleted")]
		Deleted = 4,

		/// <summary>
		/// Message has been accepted by the carrier.
		/// </summary>
		[EnumMember(Value = "accepted")]
		Accepted = 5,

		/// <summary>
		/// Message has been rejected by the carrier.
		/// </summary>
		[EnumMember(Value = "rejected")]
		Rejected = 6
	}
}
