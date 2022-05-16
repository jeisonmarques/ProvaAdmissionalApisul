using System;
using System.Collections.Generic;
using ProvaAdmissionalCSharpApisul;
using OakClass;
using System.Linq;


namespace OakClass
{
    public class Information : IElevadorService
    {
        private List<Answers> answers;
        private int nFloors;

        private static List<int> lessUsed = new List<int>();
        private static List<char> mostFrequented = new List<char>();
        private static List<char> flowElevator = new List<char>();
        private static List<char> lessFrequented = new List<char>();
       // private static 
       // private static 
       // private static 
       // private static 
        //private static 

        public Information(Reading reading, int nFloors)
        {
            this.answers = reading.GetAnswers();
            this.nFloors = nFloors;
        }


        /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary> 
        public List<int> andarMenosUtilizado()
        {
            int[] floors = recurrenceCounter();

            int menor = floors[0];

            for (int i = 0; i < nFloors; i++)
            {
                if (menor > floors[i])
                {
                    menor = floors[i];
                }
            }

            for (int i = 0; i < nFloors; i++)
            {
                if (menor == floors[i])
                {
                    lessUsed.Add(i);
                }
            }

            return lessUsed;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary> 
        public List<char> elevadorMaisFrequentado()
        {
            Dictionary<char, int> elevatorRecurrence = elevatorRecurrenceMethod();
            
            int qtdUse = 0;
            char elevadorMF = ' '; //MF - Mais Frequentado

            foreach (char c in elevatorRecurrence.Keys)
            {
                if(elevatorRecurrence.GetValueOrDefault(c) > qtdUse){
                    qtdUse = elevatorRecurrence.GetValueOrDefault(c);
                    elevadorMF = c;
                }
            }

            foreach (char c in elevatorRecurrence.Keys)
            {
                if (elevatorRecurrence.GetValueOrDefault(elevadorMF) == elevatorRecurrence.GetValueOrDefault(c))
                {
                    mostFrequented.Add(c);
                }
            }

            return mostFrequented;
        }

        /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary> 
        public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
        {
            Dictionary<char, Dictionary<char, int>> flow = new Dictionary<char, Dictionary <char, int>>();

             foreach (char f in mostFrequented)
            {
                flow.Add(f, new Dictionary <char, int>());
            }

            foreach (char f in mostFrequented)
            {
                foreach(Answers a in answers){
                    if (a.Elevador.Equals(f)  && !flow.GetValueOrDefault(f).ContainsKey(a.Turno))
                    {
                        flow.GetValueOrDefault(f).Add(a.Turno, 0);
                    }

                    if (a.Elevador.Equals(f))
                    {
                        int inc = flow.GetValueOrDefault(f).GetValueOrDefault(a.Turno) + 1;
                        flow.GetValueOrDefault(f).Remove(a.Turno);
                        flow.GetValueOrDefault(f).Add(a.Turno, inc);
                    }
                }         
            }

            return flowElevator;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
        public List<char> elevadorMenosFrequentado()
        {
            Dictionary<char, int> elevatorRecurrence = elevatorRecurrenceMethod();

            int qtdUse = int.MaxValue;
            char elevadorMF = ' '; //MF - Menos Frequentado

            foreach (char c in elevatorRecurrence.Keys)
            {
                if(elevatorRecurrence.GetValueOrDefault(c) < qtdUse){
                    qtdUse = elevatorRecurrence.GetValueOrDefault(c);
                    elevadorMF = c;
                }
            }

            foreach (char c in elevatorRecurrence.Keys)
            {
                if (elevatorRecurrence.GetValueOrDefault(elevadorMF) == elevatorRecurrence.GetValueOrDefault(c))
                {
                    lessFrequented.Add(c);
                }
            }

            return lessFrequented;
        }

        /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary> 
        public List<char> periodoMenorFluxoElevadorMenosFrequentado()
        {
            return null;
        }

        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            return null;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorA()
        {
            return 0;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorB()
        {
            return 0;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorC()
        {
            return 0;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorD()
        {
            return 0;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorE()
        {
            return 0;
        }


        private int[] recurrenceCounter()
        {
            int[] counter = new int[nFloors]; //cada posição do vetor é um andar

            for (int i = 0; i < nFloors; i++)
            {
                foreach (Answers a in answers)
                {
                    counter[a.Andar] = counter[a.Andar] + 1;
                }
            }

            return counter;
        }

        private Dictionary<char, int> elevatorRecurrenceMethod(){

            Dictionary<char, int> elevatorRecurrence = new Dictionary<char, int>();

            foreach (Answers a in answers)
            {
                if (elevatorRecurrence.ContainsKey(a.Elevador))
                {
                    int inc = elevatorRecurrence.GetValueOrDefault(a.Elevador) + 1;
                    elevatorRecurrence.Remove(a.Elevador);
                    elevatorRecurrence.Add(a.Elevador, inc);
                }
                else
                {
                    elevatorRecurrence.Add(a.Elevador, 1);
                }
            }
            
            return elevatorRecurrence;
        }

    }
}
