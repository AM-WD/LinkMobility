using System.Globalization;

namespace AMWD.Net.Api.LinkMobility
{
	internal static class SerializerExtensions
	{
		private static readonly JsonSerializerSettings _jsonSerializerSettings = new()
		{
			Culture = CultureInfo.InvariantCulture,
			Formatting = Formatting.None,
			NullValueHandling = NullValueHandling.Ignore
		};

		public static string SerializeObject(this object? obj)
			=> JsonConvert.SerializeObject(obj, _jsonSerializerSettings);

		public static T? DeserializeObject<T>(this string json)
			=> JsonConvert.DeserializeObject<T>(json, _jsonSerializerSettings);
	}
}
