using Dotnet.Sign.Domain.Aggregates.Aws;
using Dotnet.Sign.Domain.SeedWork;
using Dotnet.Sign.Domain.Aggregates.Sign;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet.Sign.Domain.Extensions
{
    public static class DomainServicesExtensions
    {
        private static void AddDomainServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAwsService, AwsService>();
            serviceCollection.AddScoped<ISignService, SignService>();
        }

        private static void AddSeedWork(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<EnvironmentKey>();
        }

        public static void AddDomain(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDomainServices();
            serviceCollection.AddSeedWork();
        }
    }
}
