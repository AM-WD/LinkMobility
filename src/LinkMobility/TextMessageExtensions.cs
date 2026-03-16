using System.Threading;
using System.Threading.Tasks;
using AMWD.Net.Api.LinkMobility.Utils;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Implementation of text messaging (SMS). <see href="https://developer.linkmobility.eu/sms-api/rest-api">API</see>
	/// </summary>
	public static class TextMessageExtensions
	{
		/// <summary>
		/// Sends a text message to a list of recipients.
		/// </summary>
		/// <param name="client">The <see cref="ILinkMobilityClient"/> instance.</param>
		/// <param name="request">The request data.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		public static Task<SendTextMessageResponse> SendTextMessage(this ILinkMobilityClient client, SendTextMessageRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (string.IsNullOrWhiteSpace(request.MessageContent))
				throw new ArgumentException("A message must be provided.", nameof(request.MessageContent));

			ValidateRecipientList(request.RecipientAddressList);
			ValidateContentCategory(request.ContentCategory);

			return client.PostAsync<SendTextMessageResponse, SendTextMessageRequest>("/smsmessaging/text", request, cancellationToken: cancellationToken);
		}

		/// <summary>
		/// Sends a binary message to a list of recipients.
		/// </summary>
		/// <param name="client">The <see cref="ILinkMobilityClient"/> instance.</param>
		/// <param name="request">The request data.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		public static Task<SendTextMessageResponse> SendBinaryMessage(this ILinkMobilityClient client, SendBinaryMessageRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (request.MessageContent == null || request.MessageContent.Count == 0)
				throw new ArgumentException("A message must be provided.", nameof(request.MessageContent));

			// Easiest way to validate that the string is a valid Base64 string.
			// Might throw a ArgumentNullException or FormatException.
			foreach (string str in request.MessageContent)
				Convert.FromBase64String(str);

			ValidateRecipientList(request.RecipientAddressList);
			ValidateContentCategory(request.ContentCategory);

			return client.PostAsync<SendTextMessageResponse, SendBinaryMessageRequest>("/smsmessaging/binary", request, cancellationToken: cancellationToken);
		}

		private static void ValidateRecipientList(IReadOnlyCollection<string>? recipientAddressList)
		{
			if (recipientAddressList == null || recipientAddressList.Count == 0)
				throw new ArgumentException("At least one recipient must be provided.", nameof(recipientAddressList));

			foreach (string recipient in recipientAddressList)
			{
				if (!Validation.IsValidMSISDN(recipient))
					throw new ArgumentException($"Recipient address '{recipient}' is not a valid MSISDN format.", nameof(recipientAddressList));
			}
		}

		private static void ValidateContentCategory(ContentCategory? contentCategory)
		{
			if (!contentCategory.HasValue)
				return;

			if (contentCategory.Value != ContentCategory.Informational && contentCategory.Value != ContentCategory.Advertisement)
				throw new ArgumentException($"Content category '{contentCategory.Value}' is not valid.", nameof(contentCategory));
		}
	}
}
