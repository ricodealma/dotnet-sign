﻿using System.Text.Json.Serialization;

namespace Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;

public sealed class ItemRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; } = default;

    [JsonPropertyName("unitPrice")]
    public double UnitPrice { get; set; } = default;
}
