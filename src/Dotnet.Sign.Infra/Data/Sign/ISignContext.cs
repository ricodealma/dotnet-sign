using Dotnet.Sign.Infra.Data.Sign.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Dotnet.Sign.Infra.Data.Sign
{
    public interface ISignContext
    {
        DbSet<ContractDTO> Contract { get; set; }
        DbSet<StatusDTO> Status { get; set; }

        DatabaseFacade Database { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
