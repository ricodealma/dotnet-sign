using Dotnet.Sign.Domain.Aggregates.Aws;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Domain.SeedWork;
using Dotnet.Sign.Domain.SeedWork.Mappers;
using Dotnet.Sign.Domain.SeedWork.EnumExtensions;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.Domain.Aggregates.Sign
{
    public sealed class SignService(
        IContractRepository contractRepository,
        EnvironmentKey environmentKey,
        IAwsService awsService) : ISignService
    {
        private readonly IContractRepository _contractRepository = contractRepository;
        private readonly EnvironmentKey _environmentKey = environmentKey;
        private readonly IAwsService _awsService = awsService;

        public async Task<Tuple<ContractModel?, ErrorResult>> InsertContractAsync(ContractRequest request, string idempotencyKey)
        {
            var contract = ContractMapper.ConvertFromRequest(request);

            var (insertedContract, error) = await _contractRepository.InsertContractAsync(contract, idempotencyKey);

            if (insertedContract == null)
                return new(null, error);

            return new(insertedContract, error);
        }
        public async Task<Tuple<ContractModel?, ErrorResult>> SelectContractByIdAsync(Guid id)
        {
            var (contract, contractError) = await _contractRepository.SelectContractByIdAsync(id);

            if (contract is null)
                return new(null, contractError);

            return new(contract, contractError);
        }

        public async Task<Tuple<ContractModel?, ErrorResult>> SendContractToSign(Guid id)
        {
            var (contract, contractError) = await _contractRepository.SelectContractByIdAsync(id);

            if (contract is null)
                return new(null, contractError);

            return new(contract, contractError);
        }
        public async Task<Tuple<ContractModel?, ErrorResult>> PostContractSigned(Guid id)
        {
            var (contract, contractError) = await _contractRepository.SelectContractByIdAsync(id);

            if (contract is null)
                return new(null, contractError);

            await Task.WhenAll(
                _awsService.PublishContractSentToSignNotificationAsync(contract),
                _awsService.PublishContractSentToSignWebhookAsync(contract)
            );

            return await UpdateStatusAsync(id, ContractStatusEnum.Signed);
        }

        public async Task<Tuple<ContractModel?, ErrorResult>> UpdateStatusAsync(Guid id, ContractStatusEnum request)
        {
            var (updateResult, updateError) = await _contractRepository.UpdateContractStatusAsync(id, request);
            if (updateResult is null)
                return new(null, updateError);

            var updateResponse = updateResult;

            return new(updateResponse, new());
        }
    }
}
