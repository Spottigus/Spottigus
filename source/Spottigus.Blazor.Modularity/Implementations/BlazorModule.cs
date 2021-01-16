using System;
using Spottigus.Blazor.Modularity.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Spottigus.Blazor.Modularity.Implementations
{
    public abstract class BlazorModule : IBlazorModule
    {
        public Assembly GetAssembly()
        {
            return typeof(BlazorModule).Assembly;
        }

        public virtual IServiceCollection RegisterServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}