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
        public IActionResult Salvar([FromBody]Usuario usuario)
        {
            return new JsonResult(JsonConvert.SerializeObject(usuario.Salvar()));
        }

        [HttpGet("Logar")]
        public IActionResult Logar(String userName, String password)
        {
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Logar(userName, password)));
        }

        [HttpPut("Editar")]
        public IActionResult Editar(int id, String userName, String lastName, DateTime data, String email, String password)
        {
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Editar(id, userName, lastName, data, email, password)));
        }

        [HttpDelete("Excluir")]
        public IActionResult Excluir(String userName, String password)
        {
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Excluir( userName, password)));
        }
    }



}
