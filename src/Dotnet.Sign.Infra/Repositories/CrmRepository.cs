using Dotnet.Sign.Domain.Aggregates.Crm;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Infra.External.Crm;

namespace Dotnet.Sign.Infra.Repositories
{
    public class CrmRepository(ICrmDAO crmDao) : ICrmRepository
    {
        private readonly ICrmDAO _crmDao = crmDao;
        public async Task<Tuple<ProposalResponse?, ErrorResult>> PostSigned(Guid id) => await _crmDao.PostSigned(id);
    }
}
