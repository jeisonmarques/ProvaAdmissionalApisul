using System;
using System.Collections.Generic;
using ProvaAdmissionalCSharpApisul;
using OakClass;
using System.Linq;
using System.Runtime;


namespace OakClass
{
    public class Information : IElevadorService
    {
        private List<Answers> answers;
        private int nFloors;

        private static List<int> lessUsed = new List<int>();
        private static List<char> mostFrequented = new List<char>();
        private static List<char> flowElevatorMaisFrequentado = new List<char>();
        private static List<char> flowElevatorMenosFrequentado = new List<char>();
        private static List<char> lessFrequented = new List<char>();
        private static List<char> periodoMaiorUtilizacao = new List<char>();
        

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

            //Procura qual é o menor
            for (int i = 0; i < nFloors; i++)
            {
                if (menor > floors[i])
                {
                    menor = floors[i];
                }
            }

            //Todos que tiveres o mesmo valor são adicionados na lista
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

            //Procuro o elevador mais frequentado comparando seus valores
            foreach (char c in elevatorRecurrence.Keys)
            {
                if(elevatorRecurrence.GetValueOrDefault(c) > qtdUse){
                    qtdUse = elevatorRecurrence.GetValueOrDefault(c);
                    elevadorMF = c;
                }
            }

            //Para cada elevador vou comparar se seu valor eh igual ao do maior encontado
            //Se for adiciona na lista, assim pega-se todos os elevadores
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
            //Dicionario de turnos e valores
            Dictionary<char, Dictionary<char, int>> flow = fluxoElevadorMethod(mostFrequented);

            
            foreach (char f in mostFrequented)
            {  
                //A partir da lista dos mais frequentados pega-se o maior valor
                int valor = flow.GetValueOrDefault(f).MaxBy(key => key.Value).Value;
                
                //Para cada key(Turno), compara com o valor encontado anteriormente
                //Se for igual, adiciona o periodo na lista
                foreach (KeyValuePair<char, int> d in flow.GetValueOrDefault(f)){

                    if(d.Value == valor){
                        flowElevatorMaisFrequentado.Add(d.Key);
                    }
                }
                
                
            }

            return flowElevatorMaisFrequentado;
        }

        /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
        public List<char> elevadorMenosFrequentado()
        {
            Dictionary<char, int> elevatorRecurrence = elevatorRecurrenceMethod();

            int qtdUse = int.MaxValue;
            char elevadorMF = ' '; //MF - Menos Frequentado

            //Procuro o elevador menos frequentado comparando seus valores
            foreach (char c in elevatorRecurrence.Keys)
            {
                if(elevatorRecurrence.GetValueOrDefault(c) < qtdUse){
                    qtdUse = elevatorRecurrence.GetValueOrDefault(c);
                    elevadorMF = c;
                }
            }

            //Para cada elevador compara-se seu valor eh igual ao do menor encontado
            //Se for adiciona na lista, assim pega-se todos os elevadores
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
            Dictionary<char, Dictionary<char, int>> flow = fluxoElevadorMethod(lessFrequented);

            foreach (char f in lessFrequented)
            {  
                //A partir da lista dos menos frequentados pega-se o maior valor
                int valor = flow.GetValueOrDefault(f).MinBy(key => key.Value).Value;
                
                //Para cada key(Turno), compara com o valor encontado anteriormente
                //Se for igual, adiciona o periodo na lista
                foreach (KeyValuePair<char, int> d in flow.GetValueOrDefault(f)){

                    if(d.Value == valor){
                        flowElevatorMenosFrequentado.Add(d.Key);
                    }
                }       
            }

            return flowElevatorMenosFrequentado;
        }

        /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
        public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
        {
            //Contar todos os turnos
            Dictionary<char, int> periodo = new Dictionary<char, int>();

            foreach(Answers a in answers){
                
                if(!periodo.ContainsKey(a.Turno)){
                    periodo.Add(a.Turno, 1);
                }else{
                    int inc = periodo.GetValueOrDefault(a.Turno) + 1;
                    periodo.Remove(a.Turno);
                    periodo.Add(a.Turno, inc);
                }
            }

            foreach (char p in periodo.Keys)
            {  
                int valor = periodo.MaxBy(key => key.Value).Value;
                
                foreach (KeyValuePair<char, int> d in periodo){

                    if(d.Value == valor){
                        if(!periodoMaiorUtilizacao.Contains(d.Key)){
                            periodoMaiorUtilizacao.Add(d.Key);
                        }
                        
                    }
                }       
            }
            
            return periodoMaiorUtilizacao;
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorA()
        {
            return percentualDeUsoElevadorMethod('A');
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorB()
        {
            return percentualDeUsoElevadorMethod('B');
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorC()
        {
            return percentualDeUsoElevadorMethod('C');
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorD()
        {
            return percentualDeUsoElevadorMethod('D');
        }

        /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary> 
        public float percentualDeUsoElevadorE()
        {
            return percentualDeUsoElevadorMethod('E');
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

        private Dictionary<char, Dictionary<char, int>> fluxoElevadorMethod(List<char> frequented){
            Dictionary<char, Dictionary<char, int>> flow = new Dictionary<char, Dictionary <char, int>>();

             foreach (char f in frequented)
            {
                flow.Add(f, new Dictionary <char, int>());
            }

            foreach (char f in frequented)
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

            return flow;
        }

        private float percentualDeUsoElevadorMethod(char c){

            float qtdUse = 0;

            foreach (Answers a in answers)
            {
                if(a.Elevador == c){
                    qtdUse++;
                }
            }

            float percentual = (qtdUse*100)/answers.Count();
            float result = (float)Math.Round(percentual, 2);

            return result;
        }

    }
}
