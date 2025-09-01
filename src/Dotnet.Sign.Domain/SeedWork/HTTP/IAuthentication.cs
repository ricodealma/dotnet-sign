namespace Dotnet.Sign.Domain.SeedWork.HTTP
{
    public interface IAuthentication
    {
        string XApiHeader { get; set; }
        string AuthorizationOrdinaryToken { get; set; }
    }
}
