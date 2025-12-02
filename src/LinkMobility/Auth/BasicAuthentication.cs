using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Implements the <see cref="IAuthentication"/> interface for BASIC authentication.
	/// </summary>
	public class BasicAuthentication : IAuthentication
	{
		private readonly string _username;
		private readonly string _password;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicAuthentication"/> class.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		public BasicAuthentication(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username))
				throw new ArgumentNullException(nameof(username), "The username cannot be null or whitespace.");

			if (string.IsNullOrWhiteSpace(password))
				throw new ArgumentNullException(nameof(password), "The password cannot be null or whitespace.");

			_username = username;
			_password = password;
		}

		/// <inheritdoc/>
		public void AddHeader(HttpClient httpClient)
		{
			string plainText = $"{_username}:{_password}";
			byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
			string base64 = Convert.ToBase64String(plainBytes);

			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64);
		}
	}
}
