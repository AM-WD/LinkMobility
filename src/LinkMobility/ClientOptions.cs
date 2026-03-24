using System.Net;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Options for the LinkMobility API.
	/// </summary>
	public class ClientOptions
	{
		/// <summary>
		/// Gets or sets the base url for the API.
		/// </summary>
		/// <remarks>
		/// The default base url is <c>https://api.linkmobility.eu/rest/</c>.
		/// </remarks>
		public virtual string BaseUrl { get; set; } = "https://api.linkmobility.eu/rest/";

		/// <summary>
		/// Gets or sets the default timeout for the API.
		/// </summary>
		/// <remarks>
		/// The default timeout is <c>100</c> seconds.
		/// </remarks>
		public virtual TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(100);

		/// <summary>
		/// Gets or sets additional default headers to every request.
		/// </summary>
		public virtual IDictionary<string, string> DefaultHeaders { get; set; } = new Dictionary<string, string>();

		/// <summary>
		/// Gets or sets additional default query parameters to every request.
		/// </summary>
		public virtual IDictionary<string, string> DefaultQueryParams { get; set; } = new Dictionary<string, string>();

		/// <summary>
		/// Gets or sets a value indicating whether to follow redirects from the server.
		/// </summary>
		public virtual bool AllowRedirects { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to use a proxy.
		/// </summary>
		public virtual bool UseProxy { get; set; }

		/// <summary>
		/// Gets or sets the proxy information.
		/// </summary>
		public virtual IWebProxy? Proxy { get; set; }
	}
}
