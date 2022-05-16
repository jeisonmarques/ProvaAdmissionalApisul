using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace OakClass
{
    public class Reading
    {
        private List<Answers> answers;

        public Reading()
        {
            answers = new List<Answers>();
        }

        public void ReadingTheAnswers(string input)
        {

            try
            {
                using(StreamReader stream = new StreamReader(input)){
                    string jsonString = stream.ReadToEnd();
                    //answers = JsonSerializer.Deserialize<List<Answers>>(jsonString);
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

        public List<Answers> GetAnswers(){
            return answers;
        }

    }
}
