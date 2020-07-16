using System;
using System.Collections.Generic;
using System.IO;
using Aula37_E_Players.Interfaces;

namespace Aula37_E_Players.Models
{
    public class Equipe : EPlayersBase ,IEquipe
    {
        public int IdEquipe { get; set;}
        public string Nome { get; set;}
        public string Imagem { get; set;}

        /// <summary>
        /// Aqui criamos a pasta csv de equipes
        /// </summary>

        private const string PATH = "Database/equipe.csv";
        public Equipe(){
            CreateFolderAndFile(PATH);
        }

        
       /// <summary>
       /// Prepara a linha para receber as informações de equipe
       /// </summary>
       /// <param name="e"></param>
      public void Create(Equipe e){
            string[] linha = { PrepararLinha (e) };
            File.AppendAllLines(PATH, linha);
        }

        private string PrepararLinha(Equipe e){
            return $"{e.IdEquipe};{e.Nome};{e.Imagem}";
        }

        /// <summary>
        ///  Cria uma lista e separa os dados de equipe por ; 
        /// </summary>
        /// <returns></returns>
        public List<Equipe> ReadAll()
        {
        {
            List<Equipe> equipes = new List<Equipe>();
            string[] linhas = File.ReadAllLines(PATH);
            foreach (var item in linhas)
            {
                string[] linha = item.Split(";");
                Equipe equipe = new Equipe();
                equipe.IdEquipe = Int32.Parse(linha[0]);
                equipe.Nome = linha[1];
                equipe.Imagem = linha[2];

                equipes.Add(equipe);
            }
            return equipes;
        }
        }

         /// <summary>
        /// Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
        /// Ela atualiza tirando o que não queremos
        /// </summary>
        /// <param name="e"></param>
        public void Update(Equipe e)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == e.IdEquipe.ToString());
            linhas.Add( PrepararLinha(e) );
            RewriteCSV(PATH, linhas);
        }

        /// <summary>
        /// Deletamos o que não queremos
        /// </summary>
        /// <param name="IdEquipe"></param>
         public void Delete(int IdEquipe)
        {
              List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == IdEquipe.ToString());
            RewriteCSV(PATH, linhas);
        }

    }
}