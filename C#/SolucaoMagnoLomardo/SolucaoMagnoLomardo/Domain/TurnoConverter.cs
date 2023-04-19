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
            TurnoConverter.Singleton,
            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        },
    };
}

internal class TurnoConverter : JsonConverter
{
    public override bool CanConvert(Type t) => t == typeof(TurnoEnum) || t == typeof(TurnoEnum?);

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var helper = (TurnoEnum) value;
        
        switch (helper)
        {
            case TurnoEnum.Matutino:
                serializer.Serialize(writer, "M");
                return;
            case TurnoEnum.Noturno:
                serializer.Serialize(writer, "N");
                return;
            case TurnoEnum.Vespertino:
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
                return TurnoEnum.Matutino;
            case "N":
                return TurnoEnum.Noturno;
            case "V":
                return TurnoEnum.Vespertino;
        }

        throw new Exception("Ocorreu um erro na desconversão do Turno");
    }

    public static readonly TurnoConverter Singleton = new TurnoConverter();
}