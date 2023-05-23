using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SmartBulaSite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace SmartBulaSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string principio_ativo)
        {

            foreach (IFormFile arq in Request.Form.Files)
            {
                string tipoArquivo = arq.ContentType;
                string extensao = System.IO.Path.GetExtension(arq.FileName);

                if (tipoArquivo.Contains("image"))
                {//se for imagem eu vou gravar no banco
                    MemoryStream s = new MemoryStream();
                    arq.CopyTo(s);
                    byte[] bytesArquivo = s.ToArray();
                    string content = await MakeRequest(bytesArquivo);

                    JToken json = JToken.Parse(content);

                    // Acessar a lista de objetos "words"
                    JArray words = (JArray)json["readResult"]["pages"][0]["words"];

                    // Iterar sobre a lista de objetos "words" e acessar o campo "content" de cada objeto
                    foreach (JObject word in words)
                    {
                        string principio = (string)word["content"];

                        if (Remedio.BuscarRemedio(principio) != null)
                        {
                            principio_ativo = principio;
                            break;
                        }
                    }
                }
                else
                {
                    principio_ativo = "Não foi possivel encontrar o nome do remedio!";
                }
            }

            TempData["Medicamento"] = principio_ativo;

            return RedirectToAction("Bula", "Home");
        }

        static async Task<String> MakeRequest(byte[] img)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "6c193a3b7d2747ae8fc02707a665fb7f");

            var uri = "https://scanremedio.cognitiveservices.azure.com/computervision/imageanalysis:analyze?api-version=2023-02-01-preview&features=read&";

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(img))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                string responseSucess = await response.Content.ReadAsStringAsync();
                return responseSucess;
            }
        }

        public IActionResult Bula()
        {
            if (TempData["Medicamento"] != null)
            {
                var principioAtivo = TempData["Medicamento"].ToString();
                return View(Remedio.BuscarRemedio(principioAtivo));
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Vitaminas()
        {
            return View();
        }

        public IActionResult Analgesicos()
        {
            return View();
        }
        public IActionResult Antiacidos()
        {
            return View();
        }
        public IActionResult Antialergicos()
        {
            return View();
        }
        public IActionResult Antibioticos()
        {
            return View();
        }
        public IActionResult AntiInflamatorio()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
