using SmartBulaSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace SmartBulaSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar() {
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Listar()));
        }
    }

    public class BuscaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Listar()
        {
            foreach (IFormFile arq in Request.Form.Files)
            {
                string tipoArquivo = arq.ContentType;
                string extensao = System.IO.Path.GetExtension(arq.FileName);

                if (tipoArquivo.Contains("image") || tipoArquivo.Contains("audio"))
                {
                    MemoryStream s = new MemoryStream();
                    arq.CopyTo(s);
                    byte[] bytesArquivo = s.ToArray();

                    
                }
                else
                {
                    
                }
            }
            return RedirectToAction("index");
        }
    }

}
