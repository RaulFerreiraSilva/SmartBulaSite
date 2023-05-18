using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmartBulaSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RemedioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Buscar([FromBody] Remedio response)
        {
            string principioAtivo = null;
            string BuscaPrincipio = null;
            JArray words = null;
            JValue teste = null;
          


            JToken json = JToken.Parse(response.ToString());
            teste = (JValue)json[""];

            if (teste == null)
            {

                // Acessar a lista de objetos "words"
                words = (JArray)json["readResult"]["pages"][0]["words"];

                // Iterar sobre a lista de objetos "words" e acessar o campo "content" de cada objeto
                foreach (JObject word in words)
                {
                    BuscaPrincipio = (string)word["content"];

                    if (Remedio.BuscarRemedio(BuscaPrincipio) != null)
                    {
                        principioAtivo = BuscaPrincipio;
                        break;
                    }
                }
            }
            else
            {
                principioAtivo = teste.ToString();
            }

            return Ok(Remedio.BuscarRemedio(principioAtivo));
        }
    }
}
