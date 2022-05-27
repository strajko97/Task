using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UserConsole.Models;
using System.IO;
using System.Linq.Expressions;

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
            try
            {
                response.EnsureSuccessStatusCode(); 
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        private async Task<int> SendFile(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                throw new Exception();
            }

            string fileName = Path.GetFileName(filePath); // check

            MultipartFormDataContent content = new MultipartFormDataContent();
            StreamContent fileStream = new StreamContent(File.OpenRead(filePath));

            content.Add(fileStream, "textFile", fileName);
            HttpResponseMessage response = await client.PostAsync("https://localhost:5001/api/Text/file", content);
            response.EnsureSuccessStatusCode();

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

        public async void GetNumberOfTextInDatabase()
        {
            try
            {
                HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/Text").Result;
                var result = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
                texts = JsonConvert.DeserializeObject<List<Text>>(result);
            }
            catch(HttpRequestException ex)
            {
                Console.WriteLine(ex.Message);
            }
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

        public static void StartProgram()
        {
            bool mainLoop = true;

            while (mainLoop)
            {
                Console.WriteLine("\n\n\n Hello user! You have 3 options to choose:");
                Console.WriteLine("1.Input string,");
                Console.WriteLine("2.Get all strings from database and choose one,");
                Console.WriteLine("3.Get string from file, and send that string to backend,");
                Console.WriteLine("4.Send file to backend!");
                int choice = 0;
                int id = 0;
                try
                {
                    Console.WriteLine("Please choose one!");
                    choice = int.Parse(Console.ReadLine());

                }
                catch (System.FormatException ex)
                {
                    Console.WriteLine("There was an error input: " + ex.Message);
                }
                int numberOfWords;
                Program program = new Program();

                {
                    switch (choice)
                    {
                        case 1:
                            
                            Console.WriteLine("Enter a string:");
                            program.text.Input = Console.ReadLine();
                            numberOfWords = program.SendText(program.text).Result;
                            Console.WriteLine("TEXT:\n{0}\n THIS TEXT HAS {1} WORDS!", program.text.Input, numberOfWords);

                            break;

                        case 2:


                            program.GetNumberOfTextInDatabase();
                            program.ShowListOfTexts();
                            Console.WriteLine("Choose one of strings from database by their Id:");
                            try
                            {
                                id = int.Parse(Console.ReadLine());
                            }
                            catch (System.FormatException ex)
                            {
                                Console.WriteLine("There was an error input: " + ex.Message);
                            }
                            try
                            {
                                program.GetTextFromDatabaseById(id);
                                numberOfWords = program.SendText(program.text).Result;
                                Console.WriteLine("TEXT:\n{0}\n THIS TEXT HAS {1} WORDS!", program.text.Input, numberOfWords);
                            }
                            catch (System.AggregateException ex)
                            {
                                Console.WriteLine("You choose invalid index and error ocurred:" + ex.Message);
                            }

                            break;
                        case 3:
                            try
                            {
                                program.text.Input = System.IO.File.ReadAllText("C:/Users/Paun/source/repos/Task2 - Copy/Text.txt");
                                numberOfWords = program.SendText(program.text).GetAwaiter().GetResult();
                                Console.WriteLine(program.text.Input + "\n THIS TEXTHAS {0} WORDS!", numberOfWords);
                            }
                            catch (System.IO.DirectoryNotFoundException ex)
                            {
                                Console.Write(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.Write(ex.Message);
                            }
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
                        default:
                            mainLoop = false;
                            break;
                    }
                }
            };
        }

        
        public static void Main(string[] args)
        {
            
            StartProgram();
        }
    }
}
