using ProvaAdmissionalCSharpApisul;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static List<PesquisaInput> pesquisaInput = new List<PesquisaInput>();
        static PesquisaInput elevadorVerificar;

        static void Main(string[] args)
        {
            ElevadorService _servico = new ElevadorService();
            LoadJsonInput(); _servico.periodoMaiorFluxoElevadorMaisFrequentado();
            Console.WriteLine("a. Qual é o andar menos utilizado pelos usuários");
            Console.WriteLine(string.Join("\t", _servico.andarMenosUtilizado()));

            Console.WriteLine("");
            Console.WriteLine("b. Qual é o elevador mais frequentado e o período que se encontra maior fluxo");
            foreach (var item in _servico.elevadorMaisFrequentado())
            {
                elevadorVerificar = new PesquisaInput { elevador = item };
                Console.WriteLine(item + " - " + string.Join("\t", _servico.periodoMaiorFluxoElevadorMaisFrequentado()));
            }

            Console.WriteLine("");
            Console.WriteLine("c. Qual é o elevador menos frequentado e o período que se encontra menor fluxo");
            foreach (var item in _servico.elevadorMenosFrequentado())
            {
                elevadorVerificar = new PesquisaInput { elevador = item };
                Console.WriteLine(item + " - " + string.Join("\t", _servico.periodoMenorFluxoElevadorMenosFrequentado()));
            }

            Console.WriteLine("");
            Console.WriteLine("d. Qual o período de maior utilização do conjunto de elevadores");
            Console.WriteLine(string.Join("\t", _servico.periodoMaiorUtilizacaoConjuntoElevadores()));

            Console.WriteLine("");
            Console.WriteLine("e. Qual o percentual de uso de cada elevador com relação a todos os serviços prestados");
            Console.WriteLine("A - " + _servico.percentualDeUsoElevadorA().ToString("N2"));
            Console.WriteLine("B - " + _servico.percentualDeUsoElevadorB().ToString("N2"));
            Console.WriteLine("C - " + _servico.percentualDeUsoElevadorC().ToString("N2"));
            Console.WriteLine("D - " + _servico.percentualDeUsoElevadorD().ToString("N2"));
            Console.WriteLine("E - " + _servico.percentualDeUsoElevadorE().ToString("N2"));

            Console.ReadKey();
        }

        static void LoadJsonInput()
        {
            pesquisaInput = JsonSerializer.Deserialize<List<PesquisaInput>>(File.ReadAllText("../../../input.json"));
        }

        class ElevadorService : IElevadorService
        {

            /// <summary> Deve retornar uma List contendo o(s) andar(es) menos utilizado(s). </summary> 
            public List<int> andarMenosUtilizado()
            {
                List<int> retorno = new List<int>();
                int countAndarMenosUtilizado = 0;
                for (int i = 0; i < 16; i++)
                    if (i == 0 || countAndarMenosUtilizado > pesquisaInput.Count(x => x.andar == i))
                        countAndarMenosUtilizado = pesquisaInput.Count(x => x.andar == i);

                for (int i = 0; i < 16; i++)
                    if (countAndarMenosUtilizado == pesquisaInput.Count(x => x.andar == i))
                        retorno.Add(i);

                return retorno;
            }

            /// <summary> Deve retornar uma List contendo o(s) elevador(es) mais frequentado(s). </summary> 
            public List<char> elevadorMaisFrequentado()
            {
                List<char> retorno = new List<char>();

                int utilizacaoElevadorA = pesquisaInput.Count(x => x.elevador == 'A'),
                    utilizacaoElevadorB = pesquisaInput.Count(x => x.elevador == 'B'),
                    utilizacaoElevadorC = pesquisaInput.Count(x => x.elevador == 'C'),
                    utilizacaoElevadorD = pesquisaInput.Count(x => x.elevador == 'D'),
                    utilizacaoElevadorE = pesquisaInput.Count(x => x.elevador == 'E'),
                    countElevadorMaisUtilizado = 0;

                countElevadorMaisUtilizado = utilizacaoElevadorA;
                if (countElevadorMaisUtilizado < utilizacaoElevadorB)
                    countElevadorMaisUtilizado = utilizacaoElevadorB;
                if (countElevadorMaisUtilizado < utilizacaoElevadorC)
                    countElevadorMaisUtilizado = utilizacaoElevadorC;
                if (countElevadorMaisUtilizado < utilizacaoElevadorD)
                    countElevadorMaisUtilizado = utilizacaoElevadorD;
                if (countElevadorMaisUtilizado < utilizacaoElevadorE)
                    countElevadorMaisUtilizado = utilizacaoElevadorE;


                if (countElevadorMaisUtilizado == utilizacaoElevadorA)
                    retorno.Add('A');
                if (countElevadorMaisUtilizado == utilizacaoElevadorB)
                    retorno.Add('B');
                if (countElevadorMaisUtilizado == utilizacaoElevadorC)
                    retorno.Add('C');
                if (countElevadorMaisUtilizado == utilizacaoElevadorD)
                    retorno.Add('D');
                if (countElevadorMaisUtilizado == utilizacaoElevadorE)
                    retorno.Add('E');

                return retorno;
            }

            /// <summary> Deve retornar uma List contendo o período de maior fluxo de cada um dos elevadores mais frequentados (se houver mais de um). </summary> 
            public List<char> periodoMaiorFluxoElevadorMaisFrequentado()
            {
                List<char> retorno = new List<char>();
                List<char> elevadores = new List<char>();
                if (elevadorVerificar == null)
                    elevadores = elevadorMaisFrequentado();
                else
                    elevadores.Add(elevadorVerificar.elevador);

                foreach (var elevador in elevadores)
                {
                    int fluxoPeriodoMatutino = pesquisaInput.Count(x => x.elevador == elevador && x.turno == 'M'),
                        fluxoPeriodoVespertino = pesquisaInput.Count(x => x.elevador == elevador && x.turno == 'V'),
                        fluxoPeriodoNoturno = pesquisaInput.Count(x => x.elevador == elevador && x.turno == 'N'),
                        fluxoPeriodoMaiorUtilizacao = 0;

                    fluxoPeriodoMaiorUtilizacao = fluxoPeriodoMatutino;
                    if (fluxoPeriodoMaiorUtilizacao < fluxoPeriodoVespertino)
                        fluxoPeriodoMaiorUtilizacao = fluxoPeriodoVespertino;
                    if (fluxoPeriodoMaiorUtilizacao < fluxoPeriodoNoturno)
                        fluxoPeriodoMaiorUtilizacao = fluxoPeriodoNoturno;

                    if (fluxoPeriodoMaiorUtilizacao == fluxoPeriodoMatutino)
                        retorno.Add('M');
                    if (fluxoPeriodoMaiorUtilizacao == fluxoPeriodoVespertino)
                        retorno.Add('V');
                    if (fluxoPeriodoMaiorUtilizacao == fluxoPeriodoNoturno)
                        retorno.Add('N');

                }

                elevadorVerificar = null;
                return retorno;
            }

            /// <summary> Deve retornar uma List contendo o(s) elevador(es) menos frequentado(s). </summary> 
            public List<char> elevadorMenosFrequentado()
            {
                List<char> retorno = new List<char>();

                int utilizacaoElevadorA = pesquisaInput.Count(x => x.elevador == 'A'),
                    utilizacaoElevadorB = pesquisaInput.Count(x => x.elevador == 'B'),
                    utilizacaoElevadorC = pesquisaInput.Count(x => x.elevador == 'C'),
                    utilizacaoElevadorD = pesquisaInput.Count(x => x.elevador == 'D'),
                    utilizacaoElevadorE = pesquisaInput.Count(x => x.elevador == 'E'),
                    countElevadorMenosUtilizado = 0;

                countElevadorMenosUtilizado = utilizacaoElevadorA;
                if (countElevadorMenosUtilizado > utilizacaoElevadorB)
                    countElevadorMenosUtilizado = utilizacaoElevadorB;
                if (countElevadorMenosUtilizado > utilizacaoElevadorC)
                    countElevadorMenosUtilizado = utilizacaoElevadorC;
                if (countElevadorMenosUtilizado > utilizacaoElevadorD)
                    countElevadorMenosUtilizado = utilizacaoElevadorD;
                if (countElevadorMenosUtilizado > utilizacaoElevadorE)
                    countElevadorMenosUtilizado = utilizacaoElevadorE;


                if (countElevadorMenosUtilizado == utilizacaoElevadorA)
                    retorno.Add('A');
                if (countElevadorMenosUtilizado == utilizacaoElevadorB)
                    retorno.Add('B');
                if (countElevadorMenosUtilizado == utilizacaoElevadorC)
                    retorno.Add('C');
                if (countElevadorMenosUtilizado == utilizacaoElevadorD)
                    retorno.Add('D');
                if (countElevadorMenosUtilizado == utilizacaoElevadorE)
                    retorno.Add('E');

                return retorno;
            }

            /// <summary> Deve retornar uma List contendo o período de menor fluxo de cada um dos elevadores menos frequentados (se houver mais de um). </summary> 
            public List<char> periodoMenorFluxoElevadorMenosFrequentado()
            {
                List<char> retorno = new List<char>();
                List<char> elevadores = new List<char>();
                if (elevadorVerificar == null)
                    elevadores = elevadorMenosFrequentado();
                else
                    elevadores.Add(elevadorVerificar.elevador);

                foreach (var elevador in elevadores)
                {
                    int fluxoPeriodoMatutino = pesquisaInput.Count(x => x.elevador == elevador && x.turno == 'M'),
                        fluxoPeriodoVespertino = pesquisaInput.Count(x => x.elevador == elevador && x.turno == 'V'),
                        fluxoPeriodoNoturno = pesquisaInput.Count(x => x.elevador == elevador && x.turno == 'N'),
                        fluxoPeriodoMenorUtilizacao = 0;

                    fluxoPeriodoMenorUtilizacao = fluxoPeriodoMatutino;
                    if (fluxoPeriodoMenorUtilizacao > fluxoPeriodoVespertino)
                        fluxoPeriodoMenorUtilizacao = fluxoPeriodoVespertino;
                    if (fluxoPeriodoMenorUtilizacao > fluxoPeriodoNoturno)
                        fluxoPeriodoMenorUtilizacao = fluxoPeriodoNoturno;

                    if (fluxoPeriodoMenorUtilizacao == fluxoPeriodoMatutino)
                        retorno.Add('M');
                    if (fluxoPeriodoMenorUtilizacao == fluxoPeriodoVespertino)
                        retorno.Add('V');
                    if (fluxoPeriodoMenorUtilizacao == fluxoPeriodoNoturno)
                        retorno.Add('N');

                }

                elevadorVerificar = null;
                return retorno;
            }

            /// <summary> Deve retornar uma List contendo o(s) periodo(s) de maior utilização do conjunto de elevadores. </summary> 
            public List<char> periodoMaiorUtilizacaoConjuntoElevadores()
            {
                List<char> retorno = new List<char>();
                int fluxoPeriodoMatutino = pesquisaInput.Count(x => x.turno == 'M'),
                    fluxoPeriodoVespertino = pesquisaInput.Count(x => x.turno == 'V'),
                    fluxoPeriodoNoturno = pesquisaInput.Count(x => x.turno == 'N'),
                    fluxoPeriodoMaiorUtilizacao = 0;

                fluxoPeriodoMaiorUtilizacao = fluxoPeriodoMatutino;
                if (fluxoPeriodoMaiorUtilizacao < fluxoPeriodoVespertino)
                    fluxoPeriodoMaiorUtilizacao = fluxoPeriodoVespertino;
                if (fluxoPeriodoMaiorUtilizacao < fluxoPeriodoNoturno)
                    fluxoPeriodoMaiorUtilizacao = fluxoPeriodoNoturno;

                if (fluxoPeriodoMaiorUtilizacao == fluxoPeriodoMatutino)
                    retorno.Add('M');
                if (fluxoPeriodoMaiorUtilizacao == fluxoPeriodoVespertino)
                    retorno.Add('V');
                if (fluxoPeriodoMaiorUtilizacao == fluxoPeriodoNoturno)
                    retorno.Add('N');

                return retorno;
            }

            /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador A em relação a todos os serviços prestados. </summary> 
            public float percentualDeUsoElevadorA()
            {
                return (float)(((pesquisaInput.Count(x => x.elevador == 'A')) * 100.0) / (pesquisaInput.Count) / 100.0);
            }

            /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador B em relação a todos os serviços prestados. </summary> 
            public float percentualDeUsoElevadorB()
            {
                return (float)(((pesquisaInput.Count(x => x.elevador == 'B')) * 100.0) / (pesquisaInput.Count) / 100.0);
            }

            /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador C em relação a todos os serviços prestados. </summary> 
            public float percentualDeUsoElevadorC()
            {
                return (float)(((pesquisaInput.Count(x => x.elevador == 'C')) * 100.0) / (pesquisaInput.Count) / 100.0);
            }

            /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador D em relação a todos os serviços prestados. </summary> 
            public float percentualDeUsoElevadorD()
            {
                return (float)(((pesquisaInput.Count(x => x.elevador == 'D')) * 100.0) / (pesquisaInput.Count) / 100.0);
            }

            /// <summary> Deve retornar um float (duas casas decimais) contendo o percentual de uso do elevador E em relação a todos os serviços prestados. </summary> 
            public float percentualDeUsoElevadorE()
            {
                return (float)(((pesquisaInput.Count(x => x.elevador == 'E')) * 100.0) / (pesquisaInput.Count) / 100.0);
            }
        }

        public class PesquisaInput
        {
            public int andar { get; set; }
            public char elevador { get; set; }
            public char turno { get; set; }
        }
    }
}
