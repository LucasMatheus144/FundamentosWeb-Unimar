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

        private List<Aluno> RetornaAlunos()
        {
            var listar = _memoria.BuscarTodos<Aluno>().ToList();
            return listar;
        }

        private Aluno RetornaId(int id)
        {
            var x = _memoria.BuscarPorId<Aluno>(id);
            return x;
        }

        public Sala IdentificaSala(int id)
        {
            var sala = _memoria.BuscarPorId<Sala>(id);
            return sala;
        }

        public ResultOperation ListarALL()
        {
            var listar = RetornaAlunos();
            return ResultOperation.Sucesso(listar);
        }

        public ResultOperation BuscarPorId(int id)
        {
            var aluno = RetornaId(id);
            if (aluno == null)
            {
                return ResultOperation.Falha("Id", "Aluno não encontrado.");
            }
            return ResultOperation.Sucesso(aluno);
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

        public ResultOperation EdicaoAluno ( DtoAlunoCreate Aluno, out List<MensagemErro> erro)
        {
            erro = new List<MensagemErro>();

            var existeAluno = RetornaId(Aluno.Id);

            if (existeAluno == null) return ResultOperation.Falha("Objeto", "O aluno digitado não existe");

            var salaAluno = IdentificaSala(Aluno.SalaId);

            if (salaAluno != null) return ResultOperation.Falha("Objeto", "O sala nova não existe");

            var transformaObjeto = new Aluno
            {
                Nome = Aluno.Nome,
                DataNascimento = Aluno.DataNascimento,
                Email = Aluno.Email,
                CPF = Aluno.Cpf,
                Sala = (salaAluno != null ) ? salaAluno : existeAluno.Sala
            };

            _memoria.Salvar(transformaObjeto);
            return ResultOperation.Sucesso(transformaObjeto);

        }

        public ResultOperation Excluir(int id)
        {
            var existe = BuscarPorId(id);

            if (existe == null)
            {
                return ResultOperation.Falha("Objeto", "Não foi encontrado o aluno para exclusão");
            }

            _memoria.Excluir(existe);
            return ResultOperation.Sucesso(existe);
        }

    }
}
