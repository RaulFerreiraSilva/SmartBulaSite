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
            return View(Usuario.Listar());
        }

        public IActionResult Buscar()
        {

            return View();
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
