using Newtonsoft.Json;

namespace SolucaoMagnoLomardo.Domain;

public class ElevatorFormInput
{
    [JsonProperty("andar")]
    public int Andar { get; set; }
    
    [JsonProperty("elevador")]
    public string Elevador { get; set; }
    
    [JsonProperty("turno")]
    public TurnoEnum Turno { get; set; }
}