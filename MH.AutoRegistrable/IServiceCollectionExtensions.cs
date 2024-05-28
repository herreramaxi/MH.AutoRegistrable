using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MH.AutoRegistrable
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoRegistrableServices(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var autoRegistrables = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x =>
                    x.IsClass &&
                    !x.IsAbstract &&
                    x.GetCustomAttribute<AutoregistrableAttribute>() is not null)
                .Select(x => x.GetCustomAttribute<AutoregistrableAttribute>());

            foreach (var autoRegistrable in autoRegistrables)
            {
                if(autoRegistrable is null) continue;

                var serviceDescriptor = new ServiceDescriptor(autoRegistrable.ServiceType, autoRegistrable.ImplementationType, autoRegistrable.ServiceLifetime);
                serviceCollection.Add(serviceDescriptor);
            }

            return serviceCollection;
        }
    }
}
