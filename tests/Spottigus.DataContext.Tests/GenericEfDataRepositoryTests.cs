using System;
using Spottigus.Tests.Helpers;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FluentAssertions;
using System.Linq;
using Spottigus.DataRepository.Implementation;
using Spottigus.ErrorHandling.Implementations;
using System.Collections.Generic;

namespace Spottigus.DataContext.Tests
{
    public class GenericEfDataRepositoryTests : SqliteInMemoryTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task Create_GoodData_CreatesXCount(int count)
        {
            GenericDataContext<Model> mockDbContext = CreateMockDbContext();
            GenericEfDataRepository<Model> sut = new GenericEfDataRepository<Model>(mockDbContext);

            for (int i = 0; i < count; i++)
            {
                Model modelToAdd = new Model() {Name = i.ToString()};
                await sut.Create(modelToAdd);   
            }

            mockDbContext.ChangeTracker.Clear();
            mockDbContext.DataSet.Count().Should().Be(count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(100)]
        public async Task GetAll_GoodData_ReturnXCount(int count)
        {
            GenericDataContext<Model> mockDbContext = CreateMockDbContext();
            GenericEfDataRepository<Model> sut = new GenericEfDataRepository<Model>(mockDbContext);

            for (int i = 0; i < count; i++)
            {
                Model modelToAdd = new Model() {Name = i.ToString()};
                mockDbContext.DataSet.Add(modelToAdd);
            }
            await mockDbContext.SaveChangesAsync();
            mockDbContext.ChangeTracker.Clear();

            var result = await sut.GetAll();
            result.Value.Should().HaveCount(count);
        }

        [Theory]
        [InlineData(1,0)]
        [InlineData(10,8)]
        [InlineData(100,42)]
        public async Task GetById_GoodData_ReturnsCorrectModel(int count, int selectedItem)
        {
            GenericDataContext<Model> mockDbContext = CreateMockDbContext();
            GenericEfDataRepository<Model> sut = new GenericEfDataRepository<Model>(mockDbContext);

            Model selectedModel = new Model();

            for (int i = 0; i < count; i++)
            {
                Model modelToAdd = new Model() {Name = i.ToString()};

                if (i == selectedItem)
                {
                    selectedModel = modelToAdd;
                }

                mockDbContext.DataSet.Add(modelToAdd);
            }

            await mockDbContext.SaveChangesAsync();
            mockDbContext.ChangeTracker.Clear();

            var result = await sut.GetById(selectedModel.Id);
            result.Value.Name.Should().Be(selectedItem.ToString());
        }

        [Fact]
        public async Task GetById_GoodData_ReturnsSuccessful()
        {
            GenericDataContext<Model> mockDbContext = CreateMockDbContext();
            GenericEfDataRepository<Model> sut = new GenericEfDataRepository<Model>(mockDbContext);

            Model selectedModel = new Model();

            for (int i = 0; i < 10; i++)
            {
                Model modelToAdd = new Model() {Name = i.ToString()};

                if (i == 5)
                {
                    selectedModel = modelToAdd;
                }

                mockDbContext.DataSet.Add(modelToAdd);
            }

            await mockDbContext.SaveChangesAsync();
            mockDbContext.ChangeTracker.Clear();

            var result = await sut.GetById(selectedModel.Id);
            result.IsSuccessful.Should().BeTrue();
        }


        [Fact]
        public async Task GetAll_GoodData_ReturnsSuccessResult()
        {
            GenericDataContext<Model> mockDbContext = CreateMockDbContext();
            GenericEfDataRepository<Model> sut = new GenericEfDataRepository<Model>(mockDbContext);

            for (int i = 0; i < 10; i++)
            {
                Model modelToAdd = new Model() {Name = i.ToString()};
                mockDbContext.DataSet.Add(modelToAdd);
            }
            await mockDbContext.SaveChangesAsync();
            mockDbContext.ChangeTracker.Clear();

            var result = await sut.GetAll();
            result.IsSuccessful.Should().BeTrue();
        }

        private GenericDataContext<Model> CreateMockDbContext()
        {
            var db = CreateInMemoryDatabase();
            var options = new DbContextOptionsBuilder().UseSqlite(db).Options;
            GenericDataContext<Model> mockDbContext = new GenericDataContext<Model>(options);
            mockDbContext.Database.EnsureCreated();
            return mockDbContext;
        }

        public class Model : IModel
        {
            public Guid Id { get; set; } = new Guid();
            public string Name { get; set; }
        }
    }
}
