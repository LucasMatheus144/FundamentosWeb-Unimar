namespace Unimar.Console.Entities
{
    public class Auth
    {
        public string User { get; init; } = "apitools";
        public string Aplication { get; init; } = "AplicationUnimar";
        public string Token { get; init; } = default!;

        public int AcessTokenMinutes { get; init; } = 10;
    }
}
