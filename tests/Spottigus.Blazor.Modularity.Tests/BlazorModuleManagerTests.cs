using System;
using System.Reflection;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Spottigus.Blazor.Modularity.Contracts;
using Spottigus.Blazor.Modularity.Implementations;
using Xunit;

namespace Spottigs.Blazor.Modularity.Tests
{
    public class BlazorModuleManagerTests
    {
        [Fact]
        public void GetAssemblies_IsEmpty_NotReturnNull()
        {
            //Arrange
            IBlazorModuleManager sut = new BlazorModuleManager(Array.Empty<IBlazorModule>());
            
            //Act
            var assemblies = sut.GetAssemblies();
        
            //Assert
            assemblies.Should().NotBeNull();

        }

        [Fact]
        public void GetAssemblies_Add1Assembly_Returns1Assemblies()
        {
            //Arrange
            IBlazorModule moduleA = NSubstitute.Substitute.For<IBlazorModule>();
            moduleA.GetAssembly().Returns(p => typeof(Exception).Assembly);

            IBlazorModuleManager sut = new BlazorModuleManager(Array.Empty<IBlazorModule>());
            sut.RegisterModule(moduleA);

            //Act
            var assemblies = sut.GetAssemblies();
        
            //Assert
            assemblies.Should().HaveCount(1);
        
        }

        [Fact]
        public void GetAssemblies_AddDuplicateAssembly_Returns1Assemblies()
        {
            //Arrange
            IBlazorModule moduleA = NSubstitute.Substitute.For<IBlazorModule>();
            moduleA.GetAssembly().Returns(p => typeof(Exception).Assembly);

            IBlazorModule moduleB = NSubstitute.Substitute.For<IBlazorModule>();
            moduleB.GetAssembly().Returns(p => typeof(Exception).Assembly);

            IBlazorModuleManager sut = new BlazorModuleManager(Array.Empty<IBlazorModule>());
            sut.RegisterModule(moduleA);
            sut.RegisterModule(moduleB);

            //Act
            var assemblies = sut.GetAssemblies();
        
            //Assert
            assemblies.Should().HaveCount(1);
        
        }

        [Fact]
        public void RegisterAssembly_NullModule_ThrowsNullArgumentException()
        {
            //Arrange
            IBlazorModuleManager sut = new BlazorModuleManager(Array.Empty<IBlazorModule>());

            //Act
            Action act = () => sut.RegisterModule(null);
        
            //Assert
            act.Should().Throw<ArgumentNullException>();
        
        }

        [Fact]
        public void RegisterServices_NullModule_ThrowsNullArgumentException()
        {
            //Arrange
            IBlazorModuleManager sut = new BlazorModuleManager(Array.Empty<IBlazorModule>());

            //Act
            Action act = () => sut.RegisterServices(null);
        
            //Assert
            act.Should().Throw<ArgumentNullException>();
        
        }
    }
}