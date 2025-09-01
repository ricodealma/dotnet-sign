using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Domain.SeedWork;
using Dotnet.Sign.Domain.SeedWork.HTTP;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dotnet.Sign.Infra.External.Crm
{
    public class CrmDAO(IRequest request, EnvironmentKey environmentKey, ILogger<CrmDAO> logger) : ICrmDAO
    {
        private readonly IRequest _request = request;
        private readonly EnvironmentKey _environmentKey = environmentKey;
        private readonly ILogger<CrmDAO> _logger = logger;

        public async Task<Tuple<ProposalResponse?, ErrorResult>> PostSigned(Guid id)
        {
            try
            {
                var response = await _request.PostAsync(
                    _environmentKey.CrmInformation.Endpoint,
                    $"v1/proposal/{id}/callbacks/signed",
                    new Authentication() { XApiHeader = _environmentKey.AppInformation.HeaderKey });
                string responseData = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    if (Enum.TryParse(((int)response.StatusCode).ToString(), out ErrorCode statusCode))
                    {
                        return new(null, new()
                        {
                            Error = true,
                            Message = responseData,
                            StatusCode = statusCode
                        });
                    }

                    return new(null, new()
                    {
                        Error = true,
                        Message = $"Unexpected error occurred while processing proposal: {responseData}",
                        StatusCode = ErrorCode.InternalServerError
                    });
                }

                var cancelResponse = JsonConvert.DeserializeObject<ProposalResponse>(responseData);
                return new(cancelResponse, new());
            }
            catch (Exception e)
            {
                _logger.LogError(JsonConvert.SerializeObject(e));
                return new(null, new()
                {
                    Error = true,
                    Message = JsonConvert.SerializeObject(e),
                    StatusCode = ErrorCode.InternalServerError
                });
            }
        }
    }
}
