using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;
using Dotnet.Sign.Domain.SeedWork.EnumExtensions;
using Newtonsoft.Json;

namespace Dotnet.Sign.Domain.SeedWork.Mappers
{
    public static class ContractMapper
    {
        public static Aggregates.Sign.Entities.Database.ContractModel ConvertFromRequest(ContractRequest contract)
        {
            return new()
            {
                Id = Guid.CreateVersion7(),
                StatusId = (int)StatusEnum.AWAITING_SIGNATURE,
                ProposalId = contract.Content.Id,
                Content = JsonConvert.SerializeObject(contract.Content),
            };
        }
    }
}
