using System.ComponentModel.DataAnnotations;
using Unimar.Console.DtoModels;

namespace Unimar.Console.Services
{
    public class ValidatorService
    {
        public bool ValidarModels<T>(T obj, out List<MensagemErro> erros)
        {
            erros = new List<MensagemErro>();
            
            if (obj == null)
            {
                erros.Add(new MensagemErro("Object", "O objeto está nulo."));
                return false;
            }

            var valida = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, new ValidationContext(obj), valida, true);

            if(!isValid)
            {
                erros = valida.Select(
                    e => new MensagemErro(
                        e.MemberNames.FirstOrDefault() ?? "Desconhecido",
                        e.ErrorMessage
                        )
                    ).ToList();
            }

            return isValid;
        }
    }
}
