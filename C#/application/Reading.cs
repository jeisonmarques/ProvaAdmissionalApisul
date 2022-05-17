using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace OakClass
{
    public class Reading
    {
        private static List<Answers> answers;

        public Reading()
        {
            answers = new List<Answers>();
        }

        //Recebe o caminho do arquivo e o desserializa usando framework Newtonsoft.Json
        //Em caso de erro mostra uma mensagem
        public void ReadingTheAnswers(string input)
        {

            try
            {
                using(StreamReader stream = new StreamReader(input)){
                    string jsonString = stream.ReadToEnd();
                    answers = JsonConvert.DeserializeObject<List<Answers>>(jsonString);
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        //Retorna a lista de objetos Answers
        public List<Answers> GetAnswers(){
            return answers;
        }

    }
}
