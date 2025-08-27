using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Sign.Infra.Data.Sign.Entities.DTOs
{
    [Table("Contract", Schema = "Sign")]
    public record ContractDTO
    {
        public Guid Id { get; set; }
        public Guid ProposalId { get; set; }
        public int StatusId { get; set; }
        public string Content { get; set; } = string.Empty;
        public StatusDTO? Status { get; set; }
    }
}
