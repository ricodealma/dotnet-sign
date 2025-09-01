namespace Dotnet.Sign.Domain.Aggregates.Aws
{
    public interface IAwsService
    {
        Task<Dictionary<string, string>> SelectSecretAsync(string? secret = null, string? region = null);
    }
}
