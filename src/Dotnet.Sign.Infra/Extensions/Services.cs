using Amazon.SimpleNotificationService;
using Dotnet.Sign.Domain.Aggregates.Aws;
using Dotnet.Sign.Domain.Aggregates.Sign;
using Dotnet.Sign.Infra.Data.Sign.Entities.DAOs;
using Dotnet.Sign.Infra.Data.Sign;
using Dotnet.Sign.Infra.External;
using Dotnet.Sign.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Amazon.SecretsManager;

namespace Dotnet.Sign.Infra.Extensions
{
    public static class InfraServicesExtensions
    {
        private static void AddDAOs(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IContractDAO, ContractDAO>();
            serviceCollection.AddScoped<IAwsDAO, AwsDAO>();
        }

        private static void AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IContractRepository, ContractRepository>();
            serviceCollection.AddScoped<IAwsRepository, AwsRepository>();
        }

        private static void AddPersistence(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IDistributedMemoryCacheDAO, DistributedMemoryCacheDAO>();
            serviceCollection.AddDbContext<ISignContext, SignContext>();
        }

        private static void AddNotification(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAWSService<IAmazonSimpleNotificationService>();
            serviceCollection.AddAWSService<IAmazonSecretsManager>();
        }

        public static void AddInfra(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDAOs();
            serviceCollection.AddRepositories();
            serviceCollection.AddPersistence();
            serviceCollection.AddNotification();
        }
    }
}
