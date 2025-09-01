namespace Dotnet.Sign.Domain.Aggregates.Aws
{
    public sealed class AwsService(IAwsRepository awsRepository) : IAwsService
    {
        private readonly IAwsRepository _awsRepository = awsRepository;

        public async Task<Dictionary<string, string>> SelectSecretAsync(string? secret = null, string? region = null) => await _awsRepository.SelectSecretAsync(secret, region);
    }
}
