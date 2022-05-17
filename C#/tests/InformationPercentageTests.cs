using Xunit;
using OakClass;

namespace InformationPercentageTests;

public class InformationPercentageTests
{
    private Information information;

    
    public InformationPercentageTests()
    {
        Reading read = new Reading();
        read.ReadingTheAnswers("../input.json");

        Information information = new Information(read, 16);
    }

    [Fact]
    public void Test_Percentage_Of_A()
    {
        float valor = information.percentualDeUsoElevadorD();
        Console.WriteLine(valor);
        Assert.Equal(34.78, valor);
    }
}
