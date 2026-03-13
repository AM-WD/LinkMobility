using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Implementation of text messaging (SMS). <see href="https://developer.linkmobility.eu/sms-api/rest-api">API</see>
	/// </summary>
	public partial class LinkMobilityClient : ILinkMobilityClient, IDisposable
	{
		/// <inheritdoc/>
		public Task<SendMessageResponse> SendTextMessage(SendTextMessageRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (string.IsNullOrWhiteSpace(request.MessageContent))
				throw new ArgumentException("A message must be provided.", nameof(request.MessageContent));

			if (request.RecipientAddressList == null || request.RecipientAddressList.Count == 0)
				throw new ArgumentException("At least one recipient must be provided.", nameof(request.RecipientAddressList));

			foreach (string recipient in request.RecipientAddressList)
			{
				if (!IsValidMSISDN(recipient))
					throw new ArgumentException($"Recipient address '{recipient}' is not a valid MSISDN format.", nameof(request.RecipientAddressList));
			}

			return PostAsync<SendMessageResponse, SendTextMessageRequest>("/smsmessaging/text", request, cancellationToken: cancellationToken);
		}

		/// <inheritdoc/>
		public Task<SendMessageResponse> SendBinaryMessage(SendBinaryMessageRequest request, CancellationToken cancellationToken = default)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			if (request.MessageContent?.Count > 0)
			{
				// Validate that the string is a valid Base64 string
				// Might throw a ArgumentNullException or FormatException
				foreach (string str in request.MessageContent)
					Convert.FromBase64String(str);
			}

			if (request.RecipientAddressList == null || request.RecipientAddressList.Count == 0)
				throw new ArgumentException("At least one recipient must be provided.", nameof(request.RecipientAddressList));

			foreach (string recipient in request.RecipientAddressList)
			{
				if (!IsValidMSISDN(recipient))
					throw new ArgumentException($"Recipient address '{recipient}' is not a valid MSISDN format.", nameof(request.RecipientAddressList));
			}

			return PostAsync<SendMessageResponse, SendBinaryMessageRequest>("/smsmessaging/binary", request, cancellationToken: cancellationToken);
		}

		// https://en.wikipedia.org/wiki/MSISDN
		private static bool IsValidMSISDN(string msisdn)
		{
			if (string.IsNullOrWhiteSpace(msisdn))
				return false;

			return Regex.IsMatch(msisdn, @"^[1-9][0-9]{7,14}$", RegexOptions.Compiled);
		}
	}
}
