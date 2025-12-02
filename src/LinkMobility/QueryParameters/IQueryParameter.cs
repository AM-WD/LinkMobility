namespace AMWD.Net.Api.LinkMobility
{
	/// <summary>
	/// Represents options defined via query parameters.
	/// </summary>
	public interface IQueryParameter
	{
		/// <summary>
		/// Retrieves the query parameters.
		IReadOnlyDictionary<string, string> GetQueryParameters();
	}
}
