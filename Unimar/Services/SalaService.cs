using Unimar.Console.DtoModels;
using Unimar.Console.Entities;
using Unimar.Console.Repository.Implementation;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (salaExistente == null)
            {
                return ResultOperation.Falha("Id", "Sala não encontrada.");
            }

            _memoria.Salvar(sala);
            return ResultOperation.Sucesso(sala);
        }

        public ResultOperation Excluir(int id)
        {
            var sala = _memoria.BuscarPorId<Sala>(id);

            if (sala == null)
            {
                return ResultOperation.Falha("Id", "Sala não encontrada.");
            }

            _memoria.Excluir(sala);
            return ResultOperation.Sucesso(sala);
        }
    }
}
