namespace Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database
{
    public record ContractModel
    {
        public Guid Id { get; set; }
        public Guid ProposalId { get; set; }
        public int StatusId { get; set; }
        public string Content { get; set; } = string.Empty;
        public StatusModel? Status { get; set; }
    }

    public record StatusModel
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
