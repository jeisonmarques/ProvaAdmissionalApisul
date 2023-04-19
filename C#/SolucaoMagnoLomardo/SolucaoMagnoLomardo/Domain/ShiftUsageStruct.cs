namespace SolucaoMagnoLomardo.Domain;

public struct ShiftUsageStruct
{
    public ShiftEnum Shift { get; set; }
    public int UsageCount { get; set; }
}