namespace SolucaoMagnoLomardo.Domain;

public class ShiftUsage
{
    public ShiftEnum Shift { get; set; }
    public int UsageCount { get; set; }
    
    public char ConvertShiftToChar()
    {
        return this.Shift switch
        {
            ShiftEnum.Matutino => 'M',
            ShiftEnum.Noturno => 'N',
            ShiftEnum.Vespertino => 'V',
            _ => throw new ArgumentOutOfRangeException()
        };
    }

}