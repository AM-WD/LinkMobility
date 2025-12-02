using System.Net.Http;
using System.Net.Http.Headers;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Implements the <see cref="IAuthentication"/> interface for BEARER authentication.
	/// </summary>
	public class AccessTokenAuthentication : IAuthentication
	{
		private readonly string _token;

		/// <summary>
		/// Initializes a new instance of the <see cref="AccessTokenAuthentication"/> class.
		/// </summary>
		/// <param name="token">The bearer token.</param>
		public AccessTokenAuthentication(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				throw new ArgumentNullException(nameof(token), "The token cannot be null or whitespace.");

			_token = token;
		}

		/// <inheritdoc/>
		public void AddHeader(HttpClient httpClient)
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
		}
	}
}
