namespace Dotnet.Sign.Domain.SeedWork.HTTP
{
    public sealed class Authentication : IAuthentication
    {
        public string XApiHeader { get; set; } = string.Empty;
        public string AuthorizationOrdinaryToken { get; set; } = string.Empty;
    }
}
