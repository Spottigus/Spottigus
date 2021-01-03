using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
//using Xunit; Need to add package references to run this example.

namespace Spottigus.Tests.Helpers.Example
{
    public class ExampleTest : SqliteInMemoryTest
    {
        //[Fact] Un-comment me if you want to run this as a test.
        public void MethodName_StateUnderTest_ExpectedBehavior()
        {
            //Arrange
            SomeDbContext mockDbContext = CreateMockDbContext();
        
            //Act
        
        
            //Assert
        
        
        }


        private SomeDbContext CreateMockDbContext()
        {
            var dbConnection = CreateInMemoryDatabase();
            var options = new DbContextOptionsBuilder().UseSqlite(dbConnection).Options;
            SomeDbContext mockDbContext = new SomeDbContext(options);
            mockDbContext.Database.EnsureCreated();
            return mockDbContext;
        }

        private class SomeDbContext : DbContext
        {
            public SomeDbContext(DbContextOptions options) : base (options)
            {
                
            }
        }
    }
}