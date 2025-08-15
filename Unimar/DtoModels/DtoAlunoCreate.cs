namespace Unimar.Console.DtoModels
{
    public class DtoAlunoCreate
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }

        public int SalaId { get; set; }
    }
}
