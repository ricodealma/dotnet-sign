using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.Domain.Aggregates.Aws
{
    public interface IAwsRepository
    {
        Task<Dictionary<string, string>> SelectSecretAsync(string? secret = null, string? region = null);
        Task PublishContractSentToSignNotificationAsync(ContractModel contract);
        Task PublishContractSentToSignWebhookAsync(ContractModel contract);
        Task PublishStatusUpdatedNotificationAsync(ContractModel contract);
        Task PublishStatusUpdatedWebhookAsync(ContractModel contract);
    }
}
