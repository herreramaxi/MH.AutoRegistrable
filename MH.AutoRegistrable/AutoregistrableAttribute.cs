using Microsoft.Extensions.DependencyInjection;

namespace MH.AutoRegistrable
{
    public class AutoregistrableAttribute: Attribute
    {   
        public required Type ServiceType { get; set; }
        public required Type ImplementationType { get; set; }
        public required ServiceLifetime ServiceLifetime { get; set; }
    }
}
