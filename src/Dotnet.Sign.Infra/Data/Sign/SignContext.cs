using Dotnet.Sign.Domain.SeedWork;
using Dotnet.Sign.Infra.Data.Sign.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Sign.Infra.Data.Sign
{
    public class SignContext(DbContextOptions<SignContext> options, EnvironmentKey environmentKey, bool test = false) : DbContext(options), ISignContext
    {
        private readonly EnvironmentKey _environmentKey = environmentKey;
        public DbSet<ContractDTO> Contract { get; set; }
        public DbSet<StatusDTO> Status { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!test)
            {
                MySqlServerVersion serverVersion = new(new Version(8, 0, 29));
                if (EnvironmentKey.TypeInformation != EnvironmentKey.Type.DEV)
                {
                    dbContextOptionsBuilder
                    .UseMySql(_environmentKey.MySqlInformation.ConnectionString, serverVersion);
                }
                else
                {
                    dbContextOptionsBuilder
                    .UseMySql(_environmentKey.MySqlInformation.ConnectionString, serverVersion)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging();
                }
            }
            else
            {
                dbContextOptionsBuilder
                    .UseInMemoryDatabase($"test_db_{Guid.NewGuid()}")
                    .EnableDetailedErrors();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContractDTO>()
                .Property(o => o.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<ContractDTO>()
                .HasOne(o => o.Status)
                .WithMany()
                .HasForeignKey(o => o.StatusId)
                .IsRequired();

        }
    }
}
