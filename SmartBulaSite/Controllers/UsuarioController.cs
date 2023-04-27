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
        [HttpPost("Salvar")]
        public IActionResult Salvar(String usuario)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(usuario);
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Salvar(user)));
        }

        [HttpGet("Logar")]
        public IActionResult Logar(String userName, String password)
        {
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Logar(userName, password)));
        }

        [HttpPut("Editar")]
        public IActionResult Editar(String usuario)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(usuario);
            return new JsonResult(JsonConvert.SerializeObject(user.Editar()));
        }

        [HttpDelete("Excluir")]
        public IActionResult Excluir(String usuario)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(usuario);
            return new JsonResult(JsonConvert.SerializeObject(user.Excluir()));
        }

        [HttpPost("Favoritar")]
        public IActionResult Favotirar(String usuario) {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(usuario);
            return new JsonResult(JsonConvert.SerializeObject(user.favoritar()));
        }

    }



}
