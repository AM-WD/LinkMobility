using System.Net.Http;

namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Defines the interface to add authentication information.
	/// </summary>
	public interface IAuthentication
	{
		/// <summary>
		/// Adds the required authentication header to the provided <see cref="HttpClient"/> instance.
		/// </summary>
		/// <param name="httpClient"></param>
		void AddHeader(HttpClient httpClient);
	}
}
