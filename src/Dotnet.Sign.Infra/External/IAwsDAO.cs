using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.Infra.External
{
    public interface IAwsDAO
    {
        Task<Dictionary<string, string>> GetSecretsFromSecretManagerAsync(string? secret = null, string? region = null);
        Task PublishContractSentToSignNotificationAsync(ContractModel contract);
        Task PublishContractSentToSignWebhookAsync(ContractModel contract);
        Task PublishStatusUpdatedNotificationAsync(ContractModel contract);
        Task PublishStatusUpdatedWebhookAsync(ContractModel contract);
    }
}
