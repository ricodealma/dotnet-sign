using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Infra.External.Crm;

namespace Dotnet.Sign.Domain.Aggregates.Sign
{
    public interface ISignService
    {
        Task<Tuple<ContractModel?, ErrorResult>> InsertContractAsync(ContractRequest contract, string idempotencyKey);
        Task<Tuple<ProposalResponse?, ErrorResult>> PostContractSigned(Guid id);
        Task<Tuple<ContractModel?, ErrorResult>> SelectContractByIdAsync(Guid id);
        Task<Tuple<ContractModel?, ErrorResult>> SendContractToSign(Guid id);
    }
}
