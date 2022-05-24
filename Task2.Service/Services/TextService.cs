using System;
using System.Collections.Generic;
using System.Text;
using Task2.Core.Model;
using Task2.Core.Repositories;
using Task2.Core.Services;

namespace Task2.Service.Services
{
   
    public class TextService : ITextService
    {   
        private readonly ITextRepository _textRepository;

        public TextService (ITextRepository textRepository)
        {
            _textRepository = textRepository;
        }
        public int NumberOFWordsInString(Text text)
        {
            return text.Input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

}
