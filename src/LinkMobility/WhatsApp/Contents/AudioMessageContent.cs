namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// A WhatsApp audio message content.
	/// </summary>
	public class AudioMessageContent : IMessageContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AudioMessageContent"/> class with the provided audio link.
		/// </summary>
		/// <param name="mediaLink">The link to an audio file (http/https only).</param>
		public AudioMessageContent(string mediaLink)
		{
			Body = new Content { Link = mediaLink };
		}

		/// <inheritdoc/>
		[JsonProperty("type")]
		public MessageType Type => MessageType.Audio;

		/// <summary>
		/// The content container.
		/// </summary>
		[JsonProperty("audio")]
		public Content Body { get; set; }

		/// <inheritdoc/>
		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(Body?.Link)
				&& (Body!.Link!.StartsWith("http://") || Body!.Link!.StartsWith("https://"));
		}

		/// <summary>
		/// Container for the audio message content.
		/// </summary>
		public class Content
		{
			/// <summary>
			/// The media link.
			/// </summary>
			[JsonProperty("link")]
			public string? Link { get; set; }

			/// <summary>
			/// A caption for the audio.
			/// </summary>
			[JsonProperty("caption")]
			public string? Caption { get; set; }
		}
	}
}
