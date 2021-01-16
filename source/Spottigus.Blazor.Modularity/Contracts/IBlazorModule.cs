using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Spottigus.Blazor.Modularity.Contracts
{
    public interface IBlazorModule
    {
        Assembly GetAssembly();
        IServiceCollection RegisterServices(IServiceCollection services);
    }
}