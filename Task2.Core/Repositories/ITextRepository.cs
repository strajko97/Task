using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;

namespace Task2.Core.Repositories
{
    public interface ITextRepository
    {
        Task<IEnumerable<Text>> GetAllTextsAsync();
        Task<Text> GetTextByIdAsync(int id);
       
    }
}
