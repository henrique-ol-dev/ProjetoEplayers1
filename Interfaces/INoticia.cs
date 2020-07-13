using System.Collections.Generic;
using Aula37_E_Players.Models;

namespace Aula37_E_Players.Interfaces
{
    public interface INoticia
    {
         void Create(Noticias n);
         List<Noticias> ReadAll();
         void Update(Noticias n);
         void Delete(int IdNoticia);

    }
}