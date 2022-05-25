using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task2.Core.Model;
using Task2.Core.Repositories;
using Task2.Data.Context;
using System.Linq;

namespace Task2.Data.Repositories
{
    public class TextRepository : ITextRepository
    {
        protected readonly MyDataBaseContext MyDataBaseContext;
        public TextRepository(MyDataBaseContext context)
         {
            MyDataBaseContext = context;
        }
        
           
       

        public async Task<IEnumerable<Text>> GetAllTextsAsync()
        {
            return await MyDataBaseContext.Texts.ToListAsync<Text>();
        }

        public async Task<Text> GetTextByIdAsync(int id)
        {
           // return await MyDataBaseContext.Texts.Where(m => m.Id == id);//da li je isto
            return await MyDataBaseContext.Texts.FindAsync(id);
        }
    }
}
