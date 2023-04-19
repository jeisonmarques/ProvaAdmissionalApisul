using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SolucaoMagnoLomardo.Domain;

public class ElevatorFormInput
{
    [JsonProperty("andar")]
    public int Andar { get; set; }
    
    [JsonProperty("elevador")]
    public string Elevador { get; set; }
    
    [JsonProperty("turno")]
    public ShiftEnum Shift { get; set; }
}