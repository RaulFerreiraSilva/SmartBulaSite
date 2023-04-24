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
    public class RemedioController
    {
            [HttpGet]
            public IActionResult Buscar(string principio_ativo)
            {
                return new JsonResult(JsonConvert.SerializeObject(Remedio.BuscarRemedio(principio_ativo)));
            }
    }
}
