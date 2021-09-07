using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamSportApi.Models;

namespace TeamSportApi.Controllers
{
    // api/teste/
    [ApiController]
    [Route("api/[controller]")]
    public class TesteController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return Ok("Meu nome");
        }

        [HttpGet]
        [Route("meunome")]
        public string MeuNome()
        {
            return "Meu nome é:";
        }

        [HttpGet]
        [Route("EnviarMsg")]
        public string EnviarMensagem(string numero,string mensagem)
        {
            return $"Enviar mensagem para o {numero} - * {mensagem} * ";
        }

        [HttpPost]
        [Route("EnviarMsg")]
        public string EnviarMensagemPorPost([FromBody] Mensagem mensagem)
        {
            return $"Enviar mensagem para o {mensagem.Numero} a mensagem * {mensagem.Msg}*";
        }
    }
}
