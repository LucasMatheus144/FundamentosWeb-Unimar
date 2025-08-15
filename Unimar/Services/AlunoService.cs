using Unimar.Console.DtoModels;
using Unimar.Console.Entities;
using Unimar.Console.Repository.Implementation;

namespace Unimar.Console.Services
{
    public class AlunoService
    {
        private readonly RepositoryMemory _memoria;
        private readonly ValidatorService _validar;
        
        public AlunoService(RepositoryMemory memoria, ValidatorService validar)
        {
            _memoria = memoria;
            _validar = validar;
        }

        public ResultOperation ListarALL()
        {
            var listar = _memoria.BuscarTodos<Aluno>();
            return ResultOperation.Sucesso(listar.ToList());
        }

        public ResultOperation BuscarPorId(int id)
        {
            var aluno = _memoria.BuscarPorId<Aluno>(id);
            if (aluno == null)
            {
                return ResultOperation.Falha("Id", "Aluno não encontrado.");
            }
            return ResultOperation.Sucesso(aluno);
        }

       
        public Sala IdentificaSala(int id)
        {
            var sala = _memoria.BuscarPorId<Sala>(id);
            return sala;
        }

        public ResultOperation Incluir(DtoAlunoCreate aluno, out List<MensagemErro> erro)
        {
            erro = new List<MensagemErro>();

            var salaAluno = IdentificaSala(aluno.SalaId);

            if (salaAluno == null)
            {
                return ResultOperation.Falha("SalaId", "Sala não encontrada.");
            }

            var criarAluno = new Aluno
            {
                Nome = aluno.Nome,
                DataNascimento = aluno.DataNascimento,
                Email = aluno.Email,
                CPF = aluno.Cpf,
                Sala = salaAluno
            };

            if (!_validar.ValidarModels<Aluno>(criarAluno, out erro)) return ResultOperation.Falha(erro);            

            _memoria.Incluir(aluno);
            return ResultOperation.Sucesso(aluno);
        }

    }
}
