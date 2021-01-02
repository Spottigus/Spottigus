using System;
using Microsoft.EntityFrameworkCore;

namespace Spottigus.DataContext
{
    public class GenericDataContext<T> : DbContext where T : class, IModel
    {
        public DbSet<T> DataSet { get; set; }

        public GenericDataContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(T).Assembly);
        }
    }
}