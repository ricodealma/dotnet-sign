using Dotnet.Sign.Domain.SeedWork.ErrorResult;

namespace Dotnet.Sign.Infra.External.Crm
{
    public interface ICrmDAO
    {
        Task<Tuple<ProposalResponse?, ErrorResult>> PostSigned(Guid id);
    }
}