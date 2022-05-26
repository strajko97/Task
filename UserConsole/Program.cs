using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserConsole.Models;
using System.IO;

namespace UserConsole
{
    public class Program
    {
        HttpClient client = new HttpClient();
        IEnumerable<Text> texts;
        Text text = new Text();
        public async Task<int> SendText(Text text)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5000/api/Text", text);
            response.EnsureSuccessStatusCode();

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        private async Task<int> SendFile(string filePath)
        {
            if(File.Exists(filePath) == false)
            {
                throw new Exception();//
            }

            string fileName = Path.GetFileName(filePath) ?? throw new InvalidDataException(); // check
           
            MultipartFormDataContent content = new MultipartFormDataContent();
            StreamContent fileStream = new StreamContent(File.OpenRead(filePath));

            content.Add(fileStream, "textFile", fileName);
            HttpResponseMessage response = await client.PostAsync("https://localhost:5001/api/Text/file", content);
            response.EnsureSuccessStatusCode();

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        public async void GetNumberOfTextInDatabase()
        {
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/Text").Result;
            var result = await response.Content.ReadAsStringAsync();
            texts = JsonConvert.DeserializeObject<List<Text>>(result);
            //radi

        }

        public async void GetTextFromDatabaseById(int id)
        {
            HttpResponseMessage response = client.GetAsync("https://localhost:5001/api/Text/" + id.ToString()).Result;

            var result = await response.Content.ReadAsStringAsync();
            text = JsonConvert.DeserializeObject<Text>(result);
        }

        public void ShowListOfTexts()
        {
            foreach (Text t in texts)
            {
                Console.WriteLine(t.Id + " " + t.Input);
            }
        }

        public async Task GetNumberOfWordFromSentFileAsync()
        {

        }


        public static void StartProgram()
        {
            bool mainLoop = true;

            while(mainLoop)
            {
                Console.WriteLine("\n\n\n Hello user! You have 3 options to choose:");
                Console.WriteLine("1.Input string,");
                Console.WriteLine("2.Get all strings from database and choose one,");
                Console.WriteLine("3.Get string from file, and send that string to backend,");
                Console.WriteLine("4.Send file to backend!");
                Console.WriteLine("Please choose one!");
                int choice = int.Parse(Console.ReadLine());
                int numberOfWords;
                Program program = new Program();

                {
                    switch (choice)
                    {
                        case 1:

                            Console.WriteLine("Enter a type (1-3):");
                            // program.text.Type = int.Parse(Console.ReadLine()); Ovdeeee sam ovo promenio

                            Console.WriteLine("Enter a string:");
                            program.text.Input = Console.ReadLine();

                            numberOfWords = program.SendText(program.text).Result;
                            Console.WriteLine("TEXT:\n{0}\n THIS TEXT HAS {1} WORDS!", program.text.Input, numberOfWords);

                            break;

                        case 2:


                            program.GetNumberOfTextInDatabase();
                            program.ShowListOfTexts();
                            Console.WriteLine("Choose one of strings from database by their Id:");
                            int id = int.Parse(Console.ReadLine());
                            // if (program.texts.Count() < id)
                            //{
                            //   throw new Exception("Id out of range");
                            //}
                            program.GetTextFromDatabaseById(id);
                            numberOfWords = program.SendText(program.text).Result;
                            Console.WriteLine("TEXT:\n{0}\n THIS TEXT HAS {1} WORDS!", program.text.Input, numberOfWords);

                            break;
                        case 3:
                            //nije dobro
                            //treba taj fajl da posaljem na backend a ne ovde da sadrzaj pretvaram u string variablu
                            program.text.Input = System.IO.File.ReadAllText("C:/Users/Paun/source/repos/Task2 - Copy/Text.txt");
                            numberOfWords = program.SendText(program.text).GetAwaiter().GetResult();
                            Console.WriteLine(program.text.Input + "\n THIS TEXTHAS {0} WORDS!", numberOfWords);

                            break;
                        case 4:
                            {
                                try
                                {
                                    numberOfWords = program.SendFile("C:/Users/Paun/source/repos/Task2 - Copy/Text.txt").GetAwaiter().GetResult();
                                    Console.WriteLine(program.text.Input + "\n THIS TEXTHAS {0} WORDS!", numberOfWords);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            break;
                        default: mainLoop = false;
                                break;
                    }
                }
            };

        }
        public static void Main(string[] args)
        {
            StartProgram();

            //Main ce da se zavrsi pre nego sto se taks zavrsi jer Main nije async
            //zato debugger nije hteo da udje
        }
    }
}
