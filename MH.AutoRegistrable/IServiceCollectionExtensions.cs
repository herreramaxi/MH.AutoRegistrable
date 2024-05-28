using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MH.AutoRegistrable
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Automatically registers services in the provided assemblies that are marked with the 
        /// <see cref="AutoRegistrableAttribute"/> into the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="serviceCollection">The service collection to which the services will be added.</param>
        /// <param name="assemblies">A collection of assemblies to scan for auto-registrable services.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with the registered services.</returns>
        public static IServiceCollection AddAutoRegistrableServices(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        {
            var autoRegistrables = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x =>
                    x.IsClass &&
                    !x.IsAbstract &&
                    x.GetCustomAttribute<AutoRegistrableAttribute>() is not null)
                .Select(x => new
                {
                    ImplementationType = x,
                    AutoRegistrableAttribute = x.GetCustomAttribute<AutoRegistrableAttribute>()
                });

            foreach (var autoRegistrable in autoRegistrables)
            {
                if (autoRegistrable?.AutoRegistrableAttribute is null ||
                    autoRegistrable?.ImplementationType is null)
                {
                    continue;
                }

                var autoRegistrableAttribute = autoRegistrable.AutoRegistrableAttribute;

                foreach (var serviceType in autoRegistrableAttribute.ServiceTypes)
                {
                    var serviceDescriptor = new ServiceDescriptor(serviceType, autoRegistrable.ImplementationType, autoRegistrableAttribute.ServiceLifetime);
                    serviceCollection.Add(serviceDescriptor);
                }
            }

            return serviceCollection;
        }
    }
}
