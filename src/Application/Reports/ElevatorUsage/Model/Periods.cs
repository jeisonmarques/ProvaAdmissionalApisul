using System.ComponentModel;

namespace Application.Reports.ElevatorUsage.Model;

public enum Periods
{
    [Description("Matutino")]
    M = 'M',
    [Description("Vespertino")]
    V = 'V',
    [Description("Noturno")]
    N = 'N'
}