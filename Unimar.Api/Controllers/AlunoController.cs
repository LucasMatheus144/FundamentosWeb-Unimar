using Microsoft.AspNetCore.Mvc;
using Unimar.Console.DtoModels;
using Unimar.Console.Services;

namespace Unimar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly AlunoService _Service;

        public AlunoController(AlunoService servico)
        {
            _Service = servico;
        }

        [HttpPost]
        public IActionResult Incluir([FromBody] DtoAlunoCreate aluno)
        {
            var cadastra = _Service.Incluir(aluno, out var erros);

            return (cadastra.Sucess is true) ? Ok(cadastra) : UnprocessableEntity(cadastra);
        }
    }
}
