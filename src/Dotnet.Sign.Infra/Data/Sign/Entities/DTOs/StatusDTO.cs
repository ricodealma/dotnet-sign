using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet.Sign.Infra.Data.Sign.Entities.DTOs
{
    [Table("Status", Schema = "Sign")]
    public record StatusDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
