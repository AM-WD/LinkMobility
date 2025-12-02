using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AMWD.Net.Api.LinkMobility.Requests;

namespace AMWD.Net.Api.LinkMobility
{
	public partial class LinkMobilityClient
	{
		/// <summary>
		/// Sends a text message to a list of recipients.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <param name="cancellationToken">A cancellation token to propagate notification that operations should be canceled.</param>
		public Task<SendTextMessageResponse> SendTextMessage(SendTextMessageRequest request, CancellationToken cancellationToken = default)
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

			return PostAsync<SendTextMessageResponse, SendTextMessageRequest>("/smsmessaging/text", request, cancellationToken: cancellationToken);
		}

		private static bool IsValidMSISDN(string msisdn)
		{
			if (string.IsNullOrWhiteSpace(msisdn))
				return false;

			return Regex.IsMatch(msisdn, @"^[1-9][0-9]{7,14}$", RegexOptions.Compiled);
		}
	}
}
