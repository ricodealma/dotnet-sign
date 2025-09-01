using Dotnet.Sign.Domain.SeedWork.EnumExtensions;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Infra.Data.Sign.Entities.DTOs;

namespace Dotnet.Sign.Infra.Data.Sign.Entities.DAOs
{
    public interface IContractDAO
    {
        Task<Tuple<ContractDTO?, ErrorResult>> InsertAsync(ContractDTO contractDTO);
        Task<Tuple<ContractDTO?, ErrorResult>> SelectByIdAsync(Guid id);
        Task<Tuple<ContractDTO?, ErrorResult>> PatchContractStatusAsync(Guid contractId, ContractStatusEnum status);

    }

}
