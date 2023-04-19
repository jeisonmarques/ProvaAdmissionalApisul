using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SolucaoMagnoLomardo.Domain;

[JsonConverter(typeof(StringEnumConverter))]
public enum ShiftEnum
{
    [EnumMember(Value = "M")]
    Matutino,
    [EnumMember(Value = "V")]
    Vespertino,
    [EnumMember(Value = "N")]
    Noturno
}