namespace Dotnet.Sign.Infra.External
{
    public interface IAwsDAO
    {
        Task<Dictionary<string, string>> GetSecretsFromSecretManagerAsync(string? secret = null, string? region = null);
    }
}
