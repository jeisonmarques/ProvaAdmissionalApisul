using System.Text.Json.Serialization;

namespace Application.Reports.ElevatorUsage.Model;

public sealed record ElevatorUse
{
    [JsonPropertyName("andar")]
    public int Floor { get; init; }

    [JsonPropertyName("elevador")]
    public Elevators Elevator { get; init; }

    [JsonPropertyName("turno")]
    public Periods Period { get; init; }
}