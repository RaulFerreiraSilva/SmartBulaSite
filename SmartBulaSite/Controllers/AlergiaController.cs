using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SmartBulaSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlergiaController : ControllerBase
    {
        [HttpPost("Salvar")]
        public IActionResult AlergiaCadastrar(String tipoAlergia)
        {
            Alergia alergia = JsonConvert.DeserializeObject<Alergia>(tipoAlergia);
            return new JsonResult(JsonConvert.SerializeObject(alergia.cadastrarAlergia()));
        }


        [HttpPost("AlergiaUsuario")]
        public IActionResult alergiaUsuario(int id_Usuario, String tipoAlergia)
        {
            Alergia alergia = JsonConvert.DeserializeObject<Alergia>(tipoAlergia);
            return new JsonResult(JsonConvert.SerializeObject(alergia.alergiaUsuario(id_Usuario)));
        }

        [HttpPost("Listar")]
        public IActionResult Listar() {
            return Ok(Alergia.listar());
        }

        [HttpPost("ListarAlergiaUsuario")]
        public IActionResult ListarAlergiaUsuario(int usuarioId) {
            return Ok(Alergia.listarAlergiaUsuario(usuarioId));
        }
    }
}
