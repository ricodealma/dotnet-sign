using Amazon.SimpleNotificationService;
using Dotnet.Sign.Domain.SeedWork;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;

namespace Dotnet.Sign.Infra.External
{
    public class AwsDAO(ILogger<AwsDAO> logger, IAmazonSimpleNotificationService snsClient, IAmazonSecretsManager secretsManager, EnvironmentKey environmentKey) : IAwsDAO
    {
        private readonly ILogger<AwsDAO> _logger = logger;
        private readonly EnvironmentKey _environmentKey = environmentKey;
        private readonly IAmazonSimpleNotificationService _snsClient = snsClient;
        private readonly IAmazonSecretsManager _secretsManager = secretsManager;

        public async Task<Dictionary<string, string>> GetSecretsFromSecretManagerAsync(string? secret = null, string? region = null)
        {
            try
            {
                var request = new GetSecretValueRequest
                {
                    SecretId = secret ?? _environmentKey.AwsInformation.SecretManagerInformation.SecretName
                };

                var response = await _secretsManager.GetSecretValueAsync(request);

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(response.SecretString) ?? [];
            }
            catch (Exception e)
            {
                _logger.LogError(JsonConvert.SerializeObject(e));
                return [];
            }
        }
    }
}
