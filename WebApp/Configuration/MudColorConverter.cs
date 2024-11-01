using System.Text.Json;
using System.Text.Json.Serialization;

using MudBlazor.Utilities;

namespace WebApp.Configuration;

public class MudColorConverter : JsonConverter<MudColor>
{
    public override MudColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            string hexValue = reader.GetString() ?? throw new JsonException("value was NULL");
            return new MudColor(hexValue);
        }
        throw new JsonException("Invalid MudColor format.");
    }

    public override void Write(Utf8JsonWriter writer, MudColor value, JsonSerializerOptions options) =>
        writer.WriteStringValue($"#{value.R:X2}{value.G:X2}{value.B:X2}");
}