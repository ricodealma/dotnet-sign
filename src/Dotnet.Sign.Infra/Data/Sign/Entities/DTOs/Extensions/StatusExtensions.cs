﻿using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.Infra.Data.Sign.Entities.DTOs.Extensions
{
    public static class StatusExtensions
    {
        public static StatusModel? ToDomain(this StatusDTO? dto) => dto is null ? null : new() { Id = dto.Id, Description = dto.Description };
    }
}
