using System.Collections.Generic;
using Aula37_E_Players.Models;

namespace Aula37_E_Players.Interfaces
{
    public interface IEquipe
    {
         void Create(Equipe e);
         List<Equipe> ReadAll();
         void Update(Equipe e);
         void Delete(int IdEquipe);
    }
}