using System.Security.Claims;

namespace Unimar.Console.Interface
{
    public interface IAuth
    {
        string GerarToken(IEnumerable<Claim> claims);
    }
}
