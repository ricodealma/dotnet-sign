using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;

namespace Dotnet.Sign.Domain.Aggregates.Sign
{
    public interface ISignService
    {
        Task<Tuple<ContractModel?, ErrorResult>> InsertContractAsync(ContractRequest contract, string idempotencyKey);
        Task<Tuple<ContractModel?, ErrorResult>> PostContractSigned(Guid id);
        Task<Tuple<ContractModel?, ErrorResult>> SelectContractByIdAsync(Guid id);
        Task<Tuple<ContractModel?, ErrorResult>> SendContractToSign(Guid id);
    }
}
