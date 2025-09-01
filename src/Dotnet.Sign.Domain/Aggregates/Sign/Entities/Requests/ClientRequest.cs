﻿using System.Text.Json.Serialization;

namespace Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;

public sealed class ClientRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("company")]
    public string Company { get; set; } = string.Empty;

}
