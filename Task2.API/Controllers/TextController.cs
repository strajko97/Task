using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;
using Task2.Core.Services;

namespace Task2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextController : ControllerBase
    {
        private readonly ITextService _textService;
        public TextController(ITextService textService)
        {
            _textService = textService;
        }

        [HttpPost("")]
        public int ReturnNumberOfWords([FromBody] Text text)
        {
            return _textService.NumberOFWordsInString(text);
        }

        [HttpGet("")]
        public async Task<IEnumerable<Text>> GetAllTexts()
        {
            return await _textService.GetAllTexts_textService();
        }

        [HttpGet("{id}")]
        public async Task<Text> GetTextById(int id)
        {
            return await _textService.GetTextById_textService(id);
        }
        
        [HttpPost("file")]
        public async Task<int> ReturnNumberOfWordsFromTextFileAsync(IFormFile textFile)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(textFile.OpenReadStream()))
            {
                while (reader.Peek() >= 0)//StearmReader.Peek() - metoda koja kaze koliko karaktera je ostalo da se procita 
                    result.AppendLine( await reader.ReadLineAsync());
            }
            Text text = new Text() { Input=result.ToString()};
            return _textService.NumberOFWordsInString(text);
        }
        
    }
}
