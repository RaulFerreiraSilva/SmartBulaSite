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
        //Metodo para Cadastrar um usuario no banco
        [HttpPost("Salvar")]
        public IActionResult Salvar(String usuario)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(usuario); //Deserealizando o envio do Mobile, para tranformar em um Usuario.
            return Ok(user.Salvar(user));
        }

        //Metodo para logar um usuario no mobile
        [HttpGet("Logar")]
        public IActionResult Logar(String email, String password)
        {
            return Ok(Usuario.Logar(email, password));
        }

        //Metodo para editar um usuario no banco
        [HttpPut("Editar")]
        public IActionResult Editar(String email, String senha, String senhaNova)
        {
            return Ok(Usuario.Editar(email, senha, senhaNova));
        }

        //Metodo para favoritar um usuario no banco
        [HttpPost("Favoritar")]
        public IActionResult Favoritar(int id_Usuario, int id_Medicamento) 
        {
            return Ok(Usuario.favoritar(id_Usuario, id_Medicamento));
        }

        //Metodo para listar os favoritos de um usuario no mobile
        [HttpGet("ListaFavoritar")]
        public IActionResult ListaFavotirar(int id_Usuario)
        {
            return Ok(Usuario.listaFavoritar(id_Usuario));
         }

        //Metodo para excluir um usuario no banco
        [HttpDelete("Excluir")]
        public IActionResult Excluir(String usuario)
        {
            Usuario user = JsonConvert.DeserializeObject<Usuario>(usuario);
            return new JsonResult(JsonConvert.SerializeObject(user.Excluir()));
        }

    }



}
