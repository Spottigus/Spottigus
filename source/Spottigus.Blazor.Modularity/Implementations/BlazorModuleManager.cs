using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Spottigus.Blazor.Modularity.Contracts;

namespace Spottigus.Blazor.Modularity.Implementations
{
    public class BlazorModuleManager : IBlazorModuleManager
    {
        private List<Assembly> _assemblies = new();
        private List<IBlazorModule> _modules = new();
        public void RegisterModule(IBlazorModule moduleToRegister)
        {
            if (moduleToRegister is null)
            {
                throw new ArgumentNullException(nameof(moduleToRegister));
            }

            _modules.Add(moduleToRegister);
        }
        public Assembly[] GetAssemblies()
        {
            return _assemblies.ToArray();
        }
        public IServiceCollection RegisterServices(IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            foreach (var module in _modules)
            {
                module.RegisterServices(services);
            }
            
            return services;
        }
    }
}