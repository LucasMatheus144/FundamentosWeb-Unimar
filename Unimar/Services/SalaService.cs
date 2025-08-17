using Unimar.Console.DtoModels;
using Unimar.Console.Entities;
using Unimar.Console.Repository.Implementation;

namespace Unimar.Console.Services
{
    public class SalaService
    {
        private readonly RepositoryMemory _memoria;
        private readonly ValidatorService _validar;

        public SalaService(RepositoryMemory memoria , ValidatorService validar)
        {
            _memoria = memoria;
            _validar = validar;
        }

        private bool ValidaSePodeExcluirSala(int idSala)
        {
            var total = (from a in _memoria.BuscarTodos<Aluno>()
                         where a.Sala.Id == idSala
                         select a.Id).Count();

            return (total > 0) ? true : false; 
        }

        public ResultOperation ListarALL()
        {
            var listar = _memoria.BuscarTodos<Sala>();

            return ResultOperation.Sucesso(listar.ToList());
        }

        public ResultOperation BuscarPorId(int id)
        {
            var sala = _memoria.BuscarPorId<Sala>(id);
            if (sala == null)
            {
                return ResultOperation.Falha("Id", "Sala não encontrada.");
            }
            return ResultOperation.Sucesso(sala);
        }

        public ResultOperation Incluir(Sala sala , out List<MensagemErro> erro)
        {
            erro = new List<MensagemErro>();

            if (!_validar.ValidarModels<Sala>(sala, out erro)) return ResultOperation.Falha(erro);

            _memoria.Incluir(sala);
            return ResultOperation.Sucesso(sala);
        }

        public ResultOperation Atualizar(Sala sala, out List<MensagemErro> erro)
        {
        
            erro = new List<MensagemErro>();
            if (!_validar.ValidarModels<Sala>(sala, out erro)) return ResultOperation.Falha(erro);

            var salaExistente = _memoria.BuscarPorId<Sala>(sala.Id);

            if (salaExistente == null) return ResultOperation.Falha("Id", "Sala não encontrada.");


            _memoria.Salvar(sala);
            return ResultOperation.Sucesso(sala);
        }

        public ResultOperation Excluir(int id)
        {
            var sala = _memoria.BuscarPorId<Sala>(id);

            if (sala == null) return ResultOperation.Falha("Id", "Sala não encontrada.");

            if (!ValidaSePodeExcluirSala(sala.Id)) return ResultOperation.Falha("Id", "Não é possível excluir a sala, pois existem alunos vinculados a ela.");

            _memoria.Excluir(sala);
            return ResultOperation.Sucesso(sala);
        }
    }
}
