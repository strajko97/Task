using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserConsole.Models;
using System.Linq;


namespace UserConsole
{
    public class Program
    {
        HttpClient client = new HttpClient();
        IEnumerable<Text> texts;
        Text t;
        public async Task<int> SendText(Text text)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5000/api/Text", text);
            response.EnsureSuccessStatusCode();

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        public async void GetNumberOfTextInDatabase()
        {
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/Text").Result;
            var result = await response.Content.ReadAsStringAsync();
            texts = JsonConvert.DeserializeObject<List<Text>>(result) as List<Text>;
            //radi
        }

        public async void GetTextFromDatabaseById(int id)
        {
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/api/Text/"+id.ToString()).Result;  

            var result = await response.Content.ReadAsStringAsync();
             t = JsonConvert.DeserializeObject<Text>(result);
        }

        public void ShowListOfTexts()
        {
            foreach (Text t in texts)
            {
                Console.WriteLine(t.Id + " " + t.Input + " " + t.Type);
            }
        }

        public static void StartProgram()
        {
            do
            {
                Console.WriteLine("\n\n\n Hello user! You have 3 options to choose:");
                Console.WriteLine("1.Input string,");
                Console.WriteLine("2.Get all strings from database and choose one,");
                Console.WriteLine("3.Get string from file.");
                Console.WriteLine("Please choose one!");
                int choice = int.Parse(Console.ReadLine());


                {
                    switch (choice)
                    {
                        case 1:
                            Text text = new Text();
                            Program p = new Program();
                            Console.WriteLine("Enter a type (1-3):");
                            text.Type = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter a string:");
                            text.Input = Console.ReadLine();
                            var z = p.SendText(text).Result;
                            Console.WriteLine("TEXT:\n{0}\n THIS TEXT HAS {1} WORDS!", p.t.Input, z);
                          

                            break;

                        case 2:

                            Program p2 = new Program();
                            p2.GetNumberOfTextInDatabase();
                            p2.ShowListOfTexts();
                            Console.WriteLine("Choose one of strings from database by their Id:");
                            int id = int.Parse(Console.ReadLine());
                            if (p2.texts.Count() < id)
                            {
                                throw new Exception("Id out of range");
                            }
                            p2.GetTextFromDatabaseById(id);
                            var x = p2.SendText(p2.t).Result;
                            Console.WriteLine("TEXT:\n{0}\n THIS TEXT HAS {1} WORDS!", p2.t.Input, x);
                            //Console.WriteLine("{0}: {1,5}", p2.t.Input, x);

                            break;
                        case 3:
                           
                            Program p3 = new Program();
                            Text text3 = new Text();
                            text3.Input= System.IO.File.ReadAllText(@"C:\Users\Paun\source\repos\Task2 - Copy\Text.txt");
                            var y = p3.SendText(text3).Result;
                            Console.WriteLine(@text3.Input+"\n THIS TEXTHAS {0} WORDS!",y);
                           
                            
                            break;
                        default:
                            return;
                    }
                }
            }
            while (true);
            
        }
        public static void Main(string[] args)
        {
            Program.StartProgram();
            
            //Main ce da se zavrsi pre nego sto se taks zavrsi jer Main nije async
            //zato debugger nije hteo da udje
        }
    }
}
