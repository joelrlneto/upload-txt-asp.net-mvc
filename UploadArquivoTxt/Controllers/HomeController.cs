using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadArquivoTxt.Models;

namespace UploadArquivoTxt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(FormCollection form)
        {
            HttpPostedFileBase arquivo = Request.Files[0];
            var uploadPath = Server.MapPath("~/Content/Uploads");
            string caminhoArquivo = Path.Combine(@uploadPath,Path.GetFileName(arquivo.FileName));
            arquivo.SaveAs(caminhoArquivo);

            var linhas = System.IO.File.ReadAllLines(caminhoArquivo);

            var produtos = new List<Produto>();

            foreach(string linha in linhas)
            {
                string codigo = linha.Substring(0, 4);
                string descricao = linha.Substring(4, 30);
                decimal valor = Decimal.Parse(linha.Substring(34, 7), NumberStyles.Currency, CultureInfo.InvariantCulture);
                produtos.Add(new Produto
                {
                    Codigo = codigo,
                    Descricao = descricao,
                    Valor = valor
                });
            }

            return View("Resultado", produtos);
        }
    }
}