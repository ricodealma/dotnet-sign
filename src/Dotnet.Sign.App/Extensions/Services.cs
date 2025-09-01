using Dotnet.Sign.App.Factory;
using Dotnet.Sign.Domain.Extensions;
using Dotnet.Sign.Domain.SeedWork.HTTP;
using Dotnet.Sign.Infra.Extensions;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;

namespace Dotnet.Sign.App.Extensions
{
    public static class ServicesExtensions
    {
        private static void AddApp(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IRequest, Request>();
            serviceCollection.AddSingleton(ConnectionMultiplexerFactory.Create);

            serviceCollection.Configure<JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            serviceCollection.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }
        public static void AddCustomServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDomain();
            serviceCollection.AddInfra();
            serviceCollection.AddApp();
        }
    }
}
