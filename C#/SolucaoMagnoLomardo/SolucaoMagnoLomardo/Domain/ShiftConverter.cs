using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SolucaoMagnoLomardo.Domain;

internal static class Converter
{
    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    {
        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        DateParseHandling = DateParseHandling.None,
        Converters =
        {
            ShiftConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}

internal class ShiftConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(ShiftEnum) || t == typeof(ShiftEnum?);

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var helper = (ShiftEnum) value;
        
        switch (helper)
        {
            case ShiftEnum.Matutino:
                serializer.Serialize(writer, "M");
                return;
            case ShiftEnum.Noturno:
                serializer.Serialize(writer, "N");
                return;
            case ShiftEnum.Vespertino:
                serializer.Serialize(writer, "V");
                return;
        }

        throw new Exception("Ocorreu um erro na conversão do Turno");

    }

    public override object ReadJson(JsonReader reader, Type t, object? existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null!;
        }

        var value = serializer.Deserialize<string>(reader);

        switch (value)
        {
            case "M":
                return ShiftEnum.Matutino;
            case "N":
                return ShiftEnum.Noturno;
            case "V":
                return ShiftEnum.Vespertino;
        }

        throw new Exception("Ocorreu um erro na desconversão do Turno");
    }

    public static readonly ShiftConverter Singleton = new ShiftConverter();
}