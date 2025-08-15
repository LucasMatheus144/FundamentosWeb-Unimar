using Microsoft.AspNetCore.Mvc;
using Unimar.Console.Entities;
using Unimar.Console.Services;

namespace Unimar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaController : ControllerBase
    {
        private readonly SalaService _servico;
        public SalaController(SalaService salaService)
        {
            _servico = salaService;
        }
        [HttpGet]
        public IActionResult Listar()
        {
            var resultado = _servico.ListarALL();

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(int id)
        {
            var procura = _servico.BuscarPorId(id);

            return (procura.Sucess is true) ? Ok(procura) : UnprocessableEntity(procura);
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] Sala sala)
        {
            var resultado = _servico.Incluir(sala, out var erros);

            return (resultado.Sucess is true) ? Ok(resultado) : UnprocessableEntity(resultado);
        }

        [HttpDelete()]
        public IActionResult Excluir([FromQuery] int id)
        {
            var resultado = _servico.Excluir(id);

            return (resultado.Sucess is true) ? Ok(resultado) : UnprocessableEntity(resultado);
        }

        [HttpPut]
        public IActionResult Atualizar([FromBody] Sala sala)
        {
            var resultado = _servico.Atualizar(sala, out var erros);

            return (resultado.Sucess is true) ? Ok(resultado) : UnprocessableEntity(resultado);
        }
    }
}
