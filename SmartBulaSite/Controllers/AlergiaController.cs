﻿using Microsoft.AspNetCore.Mvc;
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
        //Metodo reponsavel para Salvar uma alergia.
        [HttpPost("Salvar")]
        public IActionResult AlergiaCadastrar(String tipoAlergia)
        {
            Alergia alergia = JsonConvert.DeserializeObject<Alergia>(tipoAlergia);
            return Ok(alergia.cadastrarAlergia());
        }

        //Metodo responsavel para ligar uma alergia a um usuario. 
        [HttpPost("AlergiaUsuario")]
        public IActionResult alergiaUsuario(int id_Usuario, int id_Alergia)
        {
            return Ok(Alergia.alergiaUsuario(id_Usuario, id_Alergia));
        }

        //Metodo responsavel para listar as alergias.  
        [HttpGet("Listar")]
        public IActionResult Listar() {
            return Ok(Alergia.listar());
        }

        //Metodo responsavel para listar as alergias do usuario.
        [HttpGet("ListarAlergiaUsuario")]
        public IActionResult ListarAlergiaUsuario(int usuarioId)
        {
            return Ok(Alergia.listarAlergiaUsuario(usuarioId));
        }
    }
}
