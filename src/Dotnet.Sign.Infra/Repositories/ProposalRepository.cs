using Dotnet.Sign.Domain.Aggregates.Sign;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;
using Dotnet.Sign.Domain.SeedWork;
using Dotnet.Sign.Domain.SeedWork.EnumExtensions;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Infra.Data.Sign.Entities.DAOs;
using Dotnet.Sign.Infra.Data.Sign.Entities.DTOs.Extensions;
using Dotnet.Sign.Infra.External;
using Newtonsoft.Json;

namespace Dotnet.Sign.Infra.Repositories
{
    public sealed class ContractRepository(
        IContractDAO contractDAO,
        EnvironmentKey environmentKey,
        IDistributedMemoryCacheDAO distributedMemoryCacheDAO
        ) : IContractRepository

    {
        private readonly IContractDAO _contractDAO = contractDAO;
        private readonly EnvironmentKey _environmentKey = environmentKey;
        private readonly IDistributedMemoryCacheDAO _distributedMemoryCacheDAO = distributedMemoryCacheDAO;

        public async Task<Tuple<ContractModel?, ErrorResult>> InsertContractAsync(ContractModel contract)
        {
            var (result, error) = await _contractDAO.InsertAsync(contract.FromDomain());

            if (result is null)
                return new(null, error);

            return new(result.ToDomain(), new());
        }


        public async Task<Tuple<ContractModel?, ErrorResult>> SelectContractByIdAsync(Guid id)
        {
            var isInCache = _distributedMemoryCacheDAO.TryGetValue<ContractModel>(id.ToString(), out var cachedContracts);

            if (isInCache && cachedContracts is not null)
                return new(cachedContracts, new());

            var (contractDTO, error) = await _contractDAO.SelectByIdAsync(id);

            if (contractDTO is null)
                return new(null, error);


            _distributedMemoryCacheDAO.SetValue(
                $"{contractDTO.Id}",
                JsonConvert.SerializeObject(contractDTO),
                                TimeSpan.FromMinutes(_environmentKey.RedisInformation.CacheExpirationTime));

            return new(contractDTO.ToDomain(), new());
        }

        public async Task<Tuple<ContractModel?, ErrorResult>> UpdateContractStatusAsync(Guid id, ContractStatusEnum request)
        {
            var (updatedContract, updateError) = await _contractDAO.PatchContractStatusAsync(id, request);

            if (updatedContract is null)
                return new(null, updateError);

            return new(updatedContract.ToDomain(), new());
        }
    }
}
