using Dotnet.Sign.Domain.Aggregates.Aws;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;
using Dotnet.Sign.Infra.External;

namespace Dotnet.Sign.Infra.Repositories
{
    public sealed class AwsRepository(IAwsDAO aws) : IAwsRepository
    {
        private readonly IAwsDAO _awsDao = aws;

        public async Task<Dictionary<string, string>> SelectSecretAsync(string? secret = null, string? region = null) => await _awsDao.GetSecretsFromSecretManagerAsync(secret, region);
        public async Task PublishContractSentToSignNotificationAsync(ContractModel contract) => await _awsDao.PublishContractSentToSignNotificationAsync(contract);
        public async Task PublishContractSentToSignWebhookAsync(ContractModel contract) => await _awsDao.PublishContractSentToSignWebhookAsync(contract);
        public async Task PublishStatusUpdatedNotificationAsync(ContractModel contract) => await _awsDao.PublishStatusUpdatedNotificationAsync(contract);
        public async Task PublishStatusUpdatedWebhookAsync(ContractModel contract) => await _awsDao.PublishStatusUpdatedWebhookAsync(contract);
    }
}

