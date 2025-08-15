using System.ComponentModel.DataAnnotations;
using Unimar.Console.Interface;
using Unimar.Console.ValuesObjects;

namespace Unimar.Console.Entities
{
    public class Sala: IEntidade
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public int Capacidade { get; set; }

        public string Descricao { get; set; }

        public SituacaoSala Status { get; set; }
    }
}
