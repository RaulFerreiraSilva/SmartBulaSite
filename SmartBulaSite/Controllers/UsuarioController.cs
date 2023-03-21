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
        public IActionResult Logar( String userName, String password) {
            return new JsonResult(JsonConvert.SerializeObject(Usuario.Logar(userName, password)));
        }
    }



}
