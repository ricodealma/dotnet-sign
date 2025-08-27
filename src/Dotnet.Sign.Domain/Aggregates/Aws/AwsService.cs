using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.Domain.Aggregates.Aws
{
    public sealed class AwsService(IAwsRepository awsRepository) : IAwsService
    {
        private readonly IAwsRepository _awsRepository = awsRepository;

        public async Task<Dictionary<string, string>> SelectSecretAsync(string? secret = null, string? region = null) => await _awsRepository.SelectSecretAsync(secret, region);
        public async Task PublishContractSentToSignNotificationAsync(ContractModel contract) => await _awsRepository.PublishContractSentToSignNotificationAsync(contract);
        public async Task PublishContractSentToSignWebhookAsync(ContractModel contract) => await _awsRepository.PublishContractSentToSignWebhookAsync(contract);

        public async Task PublishStatusUpdatedNotificationAsync(ContractModel contract) => await _awsRepository.PublishStatusUpdatedNotificationAsync(contract);
        public async Task PublishStatusUpdatedWebhookAsync(ContractModel contract) => await _awsRepository.PublishStatusUpdatedWebhookAsync(contract);
    }
}
