using System.Security.Claims;
using Unimar.Console.Entities;

namespace Unimar.Console.Interface
{
    public interface IAuth
    {
        string GerarToken(IEnumerable<Claim> claims);

        Auth RetornaTokenValido();
    }
}
