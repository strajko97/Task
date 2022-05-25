using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;

namespace Task2.Core.Services
{
    public interface ITextService
    {
        int NumberOFWordsInString(Text text);

        Task<IEnumerable<Text>> GetAllTexts_textService();

        Task<Text> GetTextById_textService(int id);
    }
}
