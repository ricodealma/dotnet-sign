using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;
using Dotnet.Sign.Domain.SeedWork.EnumExtensions;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;

namespace Dotnet.Sign.Domain.Aggregates.Sign
{
    public interface IContractRepository
    {
        Task<Tuple<ContractModel?, ErrorResult>> InsertContractAsync(ContractModel contract);
        Task<Tuple<ContractModel?, ErrorResult>> SelectContractByIdAsync(Guid id);
        Task<Tuple<ContractModel?, ErrorResult>> UpdateContractStatusAsync(Guid id, ContractStatusEnum request);
    }
}
