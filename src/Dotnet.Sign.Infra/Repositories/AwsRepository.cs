using Dotnet.Sign.Domain.Aggregates.Aws;
using Dotnet.Sign.Infra.External;

namespace Dotnet.Sign.Infra.Repositories
{
    public sealed class AwsRepository(IAwsDAO aws) : IAwsRepository
    {
        private readonly IAwsDAO _awsDao = aws;

        public async Task<Dictionary<string, string>> SelectSecretAsync(string? secret = null, string? region = null) => await _awsDao.GetSecretsFromSecretManagerAsync(secret, region);
    }
}

