using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Spottigus.Blazor.Modularity.Contracts
{
    public interface IBlazorModuleManager
    {
        void RegisterModule(IBlazorModule moduleToRegister);
        Assembly[] GetAssemblies();
        IServiceCollection RegisterServices(IServiceCollection services);
    }
}