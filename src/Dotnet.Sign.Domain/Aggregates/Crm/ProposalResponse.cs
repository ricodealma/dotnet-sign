namespace Dotnet.Sign.Infra.External.Crm;

public sealed class ProposalResponse
{
    public Guid Id { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<ItemResponse> Items { get; set; } = [];
    public ClientResponse Client { get; set; } = new();
}

public sealed class ItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
}

public sealed class ClientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
}
