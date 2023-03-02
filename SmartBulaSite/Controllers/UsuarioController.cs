using SmartBulaSite.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartBulaSite.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Listar() {
            return View(Usuario.Listar());
        }
    }
}
