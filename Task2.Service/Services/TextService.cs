using System;
using System.Collections.Generic;
using System.Text;
using Task2.Core.Model;
using Task2.Core.Services;

namespace Task2.Service.Services
{
    public class TextService : ITextService
    {
        public int NumberOFWordsInString(Text text)
        {
            return text.Input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }

}
