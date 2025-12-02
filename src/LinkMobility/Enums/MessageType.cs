namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Specifies the message type.
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// The message is sent as defined in the account settings.
		/// </summary>
		Default = 1,

		/// <summary>
		/// The message is sent as voice call.
		/// </summary>
		Voice = 2,
	}
}
