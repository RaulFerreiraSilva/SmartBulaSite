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
        //Metodo responsavel, para buscar do remedio do banco.
        [HttpGet]
        public IActionResult Buscar(string response) //Espera receber o nome do remedio.
        {
            return Ok(Remedio.BuscarRemedio(response));
        }
    }
}
