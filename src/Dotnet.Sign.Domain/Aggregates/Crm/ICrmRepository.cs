using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Infra.External.Crm;

namespace Dotnet.Sign.Domain.Aggregates.Crm
{
    public interface ICrmRepository
    {
        Task<Tuple<ProposalResponse?, ErrorResult>> PostSigned(Guid id);
    }
}
