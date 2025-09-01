using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.Infra.Data.Sign.Entities.DTOs.Extensions
{
    public static class ContractExtensions
    {
        public static ContractDTO FromDomain(this ContractModel contract)
        {
            return new()
            {
                Id = contract.Id,
                StatusId = contract.StatusId,
                Content = contract.Content,
                ProposalId = contract.ProposalId,
            };
        }

        public static ContractModel ToDomain(this ContractDTO dto)
        {
            return new ContractModel
            {
                Id = dto.Id,
                StatusId = dto.StatusId,
                Content = dto.Content,
                Status = dto.Status?.ToDomain(),
                ProposalId = dto.ProposalId
            };
        }

        public static List<ContractModel> ToDomain(this List<ContractDTO> contractDTOs) => contractDTOs.Select(ToDomain).ToList();
    }
}
