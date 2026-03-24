namespace AMWD.Net.Api.LinkMobility.Utils
{
	internal class UnixTimestampJsonConverter : JsonConverter
	{
		private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public override bool CanConvert(Type objectType)
		{
			return typeof(DateTime?).IsAssignableFrom(objectType);
		}

		public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
		{
			long? ts = serializer.Deserialize<long?>(reader);

			if (ts.HasValue)
				return _unixEpoch.AddSeconds(ts.Value);

			return null;
		}

		public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			if (value is DateTime dt)
			{
				long unixTimestamp = (long)(dt.ToUniversalTime() - _unixEpoch).TotalSeconds;
				writer.WriteValue(unixTimestamp);
				return;
			}

			throw new JsonSerializationException("Expected date object value.");
		}
	}
}
