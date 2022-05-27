using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
           ///////////////////////////////////////////////////
        {
            return text.Input.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public async Task<IEnumerable<Text>> GetAllTexts_textService()
        {
            return await _textRepository.GetAllTextsAsync();//ovde mogu da dodam try cathch, ako je prazna baza
        }

        public async Task<Text> GetTextById_textService(int id)
        {
            return await _textRepository.GetTextByIdAsync(id);//ako se ne nadje sa tim id-jem
        }
    }
}
