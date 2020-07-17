using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aula37_E_Players.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Aula37_E_Players.Controllers
{
    public class NoticiaController : Controller
    {
        Noticias noticiaModel = new Noticias();
        public IActionResult Index()
        {
            ViewBag.Noticias = noticiaModel.ReadAll();
            return View();
        }

        /// <summary>
        /// Cadastra um formulário e retorna ele na index
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
           public IActionResult Cadastrar(IFormCollection form)
        {
            Noticias novaNoticia   = new Noticias();
            novaNoticia.IdNoticia = Int32.Parse(form["IdNoticia"]);
            novaNoticia.Titulo     = form["Titulo"];
            novaNoticia.Texto      = form["Texto"];

            // Upload Início
            var file    = form.Files[0];
            var folder  = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Noticia");

            if(file != null)
            {
                if(!Directory.Exists(folder)){
                    Directory.CreateDirectory(folder);
                }

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/", folder, file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))  
                {  
                    file.CopyTo(stream);  
                }
                novaNoticia.Imagem   = file.FileName;
            }
            else
            {
                novaNoticia.Imagem   = "padrao.png";
            }
            // Upload Final

            noticiaModel.Create(novaNoticia);
            ViewBag.Noticias = noticiaModel.ReadAll();

            return LocalRedirect("~/Noticia");
        }
            
        /// <summary>
        /// Exclui o que não queremos já atualizando
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
           [Route("[controller]/{id}")]
        public IActionResult Excluir(int id)
        {
            noticiaModel.Delete(id);
            ViewBag.Noticias = noticiaModel.ReadAll();
            return LocalRedirect("~/Noticia");
        }


    }
}