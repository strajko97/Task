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
        //Zamisao: Korisnik unosi da li hoce da sam unosi tekst,
        //         -da li bira bazu(2) tj da dobije Listu svih tekstova koji postoje u bazi
        //         i da na osvnu id-ja odabere odgovarajuci text i da mu se za taj teks 
        //         izracuna koliko ima reci
        //         -bira da li hoce iz tekst iz fajla (3): taj tekst salje na backend i tada salje taj tekst
        //          i za njega dobija koliko ima reci
    }
}
