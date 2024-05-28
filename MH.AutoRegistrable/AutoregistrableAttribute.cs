using Microsoft.Extensions.DependencyInjection;

namespace MH.AutoRegistrable
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRegistrableAttribute : Attribute
    {
        public ServiceLifetime ServiceLifetime { get; set; }
        public Type[] ServiceTypes { get; set; }

        public AutoRegistrableAttribute(ServiceLifetime serviceLifetime, params Type[] serviceTypes)
        {
            this.ServiceLifetime = serviceLifetime;
            this.ServiceTypes = serviceTypes;
        }
    }
}
