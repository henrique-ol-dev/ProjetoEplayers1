using System;
using System.Collections.Generic;
using System.IO;

namespace Aula37_E_Players.Models
{
    public class Noticias : EPlayersBase
    {
       
        public Noticias(int idNoticia, string titulo, string texto, string imagem)
        {
            this.IdNoticia = idNoticia;
            this.Titulo = titulo;
            this.Texto = texto;
            this.Imagem = imagem;

        }
        public int IdNoticia { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public string Imagem { get; set; }
        
        /// <summary>
        /// Aqui criamos a pasta csv de noticias
        /// </summary>
        private const string PATH = "Database/noticia.csv";
        public Noticias()
        {
            CreateFolderAndFile(PATH);
        }

        /// <summary>
        /// Prepara a linha para receber as informações da noticia
        /// </summary>
        /// <param name="n"></param>
        public void Create(Noticias n)
        {
            string[] linha = { PrepararLinha(n) };
            File.AppendAllLines(PATH, linha);
        }

        /// <summary>
        /// Recebe as informações de noticia
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private string PrepararLinha(Noticias n)
        {
            return $"{n.IdNoticia};{n.Titulo};{n.Texto};{n.Imagem}";
        }


        /// <summary>
        /// Cria uma lista e separa os dados da noticia por ; 
        /// </summary>
        /// <returns></returns>
        public List<Noticias> ReadAll()
        {

            {
                List<Noticias> noticias = new List<Noticias>();
                string[] linhas = File.ReadAllLines(PATH);
                foreach (var item in linhas)
                {
                    string[] linha = item.Split(";");

                    Noticias noticia = new Noticias();
                    noticia.IdNoticia = Int32.Parse(linha[0]);
                    noticia.Titulo = linha[1];
                    noticia.Texto = linha[2];
                    noticia.Imagem = linha[3];

                    noticias.Add(noticia);
                }
                return noticias;
            }
        }


        /// <summary>
        /// Criamos uma lista que servirá como uma espécie de backup para as linhas do csv
        /// Ela atualiza tirando o que não queremos
        /// </summary>
        /// <param name="n"></param>
        public void Update(Noticias n)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == n.IdNoticia.ToString());
            linhas.Add(PrepararLinha(n));
            RewriteCSV(PATH, linhas);
        }
        /// <summary>
        /// Deletamos o que não queremos
        /// </summary>
        /// <param name="IdNoticia"></param>
        public void Delete(int IdNoticia)
        {
            List<string> linhas = ReadAllLinesCSV(PATH);
            linhas.RemoveAll(x => x.Split(";")[0] == IdNoticia.ToString());
            RewriteCSV(PATH, linhas);
        }

    }
}