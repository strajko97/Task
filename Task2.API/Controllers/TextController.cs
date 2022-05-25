using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
