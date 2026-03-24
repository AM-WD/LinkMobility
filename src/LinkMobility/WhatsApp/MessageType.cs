using System.Runtime.Serialization;
using Newtonsoft.Json.Converters;

namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// Represents the list of supported message types for WhatsApp messages.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum MessageType
	{
		/// <summary>
		/// Send a simple text message.
		/// </summary>
		[EnumMember(Value = "text")]
		Text = 1,

		/// <summary>
		/// Sends a media message, which contains the link to an image.
		/// </summary>
		[EnumMember(Value = "image")]
		Image = 2,

		/// <summary>
		/// Sends a media message, which contains the link to a video.
		/// </summary>
		[EnumMember(Value = "video")]
		Video = 3,

		/// <summary>
		/// Sends a media message, which contains the link to an audio file.
		/// </summary>
		[EnumMember(Value = "audio")]
		Audio = 4,

		/// <summary>
		/// Sends a media message, which contains the link to a document (e.g. PDF).
		/// </summary>
		[EnumMember(Value = "document")]
		Document = 5,
	}
}
