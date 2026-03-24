namespace AMWD.Net.Api.LinkMobility.WhatsApp
{
	/// <summary>
	/// A WhatsApp video message content.
	/// </summary>
	public class VideoMessageContent : IMessageContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="VideoMessageContent"/> class with the provided video link.
		/// </summary>
		/// <param name="mediaLink">The link to a video (http/https only).</param>
		public VideoMessageContent(string mediaLink)
		{
			Body = new Content { Link = mediaLink };
		}

		/// <inheritdoc/>
		[JsonProperty("type")]
		public MessageType Type => MessageType.Video;

		/// <summary>
		/// The content container.
		/// </summary>
		[JsonProperty("video")]
		public Content Body { get; set; }

		/// <inheritdoc/>
		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(Body?.Link)
				&& (Body!.Link!.StartsWith("http://") || Body!.Link!.StartsWith("https://"));
		}

		/// <summary>
		/// Container for the text message content.
		/// </summary>
		public class Content
		{
			/// <summary>
			/// The message text.
			/// </summary>
			[JsonProperty("link")]
			public string? Link { get; set; }

			/// <summary>
			/// A caption for the image.
			/// </summary>
			[JsonProperty("caption")]
			public string? Caption { get; set; }
		}
	}
}
