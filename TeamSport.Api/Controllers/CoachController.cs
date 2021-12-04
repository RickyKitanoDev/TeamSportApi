using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamSportApi.DataBase;
using TeamSportApi.Models;

namespace TeamSportApi.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private static readonly CoachDataBase dataBase = new CoachDataBase();
        //MÉTODO PARA RETORNAR TODOS OS TREINADORES
        [HttpGet("GetCoachs")]
        public ActionResult<List<Coach>> GetCoachs()
        {
            var coachs = dataBase.GetCoachsDataBase();
            if (coachs.Count == 0)
                return BadRequest(new { result = new List<Coach>(), message = "Não foi encontrado nenhum treinador", TemTreinador = false });
            return Ok(new { result = coachs, message = $"Treinadores encontrados = {coachs.Count}", TemTreinador = false });
        }

        //MÉTODO PARA RETORNAR TREINADOR PELO ID
        [HttpGet("{id}")]
        public ActionResult GetCoachById(int id)
        {
            Coach coach = dataBase.GetCoachById(id);
            bool coachIsValid = dataBase.VerifyCoachExists(id);
            if (coachIsValid == false)
                return BadRequest(new { message = "Não foi encontrado treinador para o Id informado. " });
            return Ok(coach);
        }
        //MÉTODO PARA RETORNAR TREINADOR PELO NOME
        [HttpGet("getname/{name}")]
        public ActionResult GetCoachName(string name)
        {
            Coach coach = dataBase.GetCoachByName(name);
            bool coachIsValid = dataBase.VerifyCoachExistsName(name);
            if (coachIsValid == true)
                return Ok(coach);
            return BadRequest(new { message = "Não foi encontrado treinador para o nome informado." });
        }
    }
}
