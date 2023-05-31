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
                        string principio = (string)word["content"]; // Coloca as palavras presentes no contente dentro da variavel principio.

                        if (Remedio.BuscarRemedio(principio) != null) // Checa se a palavra encontrada, existe no nosso banco de dados, como principio_ativo.
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

            TempData["Medicamento"] = principio_ativo; // Armezana em um temp data para passar para a outra tela.

            return RedirectToAction("Bula", "Home");
        }

        //Metodo responsavel por fazer a chamada e retirada do texto da imagem, api da Azure.
        static async Task<String> MakeRequest(byte[] img)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers - Chave de conexão para ter acesso a api.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "6c193a3b7d2747ae8fc02707a665fb7f");

            var uri = "https://scanremedio.cognitiveservices.azure.com/computervision/imageanalysis:analyze?api-version=2023-02-01-preview&features=read&"; // Url para conexão da api.

            HttpResponseMessage response;

            using (var content = new ByteArrayContent(img))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream"); // Avisa a api que ela ira receber um arquivo.
                response = await client.PostAsync(uri, content);//Utiliza o meotodo HttpClient(), para realizar a consulta na api da azure, com a url e a imagem, convertida em um array de Bytes.
                string responseSucess = await response.Content.ReadAsStringAsync();// realiza a consulta na api da azure.
                return responseSucess;
            }
        }

        public IActionResult Bula()
        {
            if (TempData["Medicamento"] != null) //Chega se existe um principio ativo no tempData
            {
                var principioAtivo = TempData["Medicamento"].ToString(); // retira o que esta no tempData e o torna em String.
                return View(Remedio.BuscarRemedio(principioAtivo)); //Retorna a bula para a tela
            }

            return RedirectToAction("Index", "Home"); //Caso não exista um principio ativo, retorna para a tela de busca.
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
