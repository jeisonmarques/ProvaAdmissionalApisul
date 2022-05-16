using System;
using OakClass;
using System.Collections.Generic;

namespace CSharp.application
{
    public class Program
    {
        public static void Main()
        {

            Reading r = new Reading();
            r.ReadingTheAnswers("../input.json");
            
            Information i = new Information(r, 16);
            List<char> list1 = i.elevadorMaisFrequentado();
            List<char> list = i.periodoMaiorFluxoElevadorMaisFrequentado();
            foreach(char a in list){
                Console.Write("{0} - ", a);
            }

            
        }

    }
}
