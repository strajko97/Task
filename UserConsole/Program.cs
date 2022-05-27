using System;
using System.Net.Http;
using System.Threading.Tasks;
using UserConsole.Models;



namespace UserConsole
{
    class Program
    {
         HttpClient client = new HttpClient();
         public async Task<int> SendText(Text text)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5000/api/Text", text);
            response.EnsureSuccessStatusCode();

            return Convert.ToInt32(await response.Content.ReadAsStringAsync());
        }

       public static void Main(string[] args)
        {
            Console.WriteLine("Hello user!");
            Text text = new Text();
            Console.WriteLine("Enter a type (1-3):");
            text.Type = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter a string:");
            text.Input = Console.ReadLine();
            Program p = new Program();
            var numberOfWords = p.SendText(text).Result;

            Console.WriteLine("{0}: {1,5}", text.Input, numberOfWords);
            //Main ce da se zavrsi pre nego sto se taks zavrsi jer Main nije async
            //zato debugger nije hteo da udje
         }
    }
}
