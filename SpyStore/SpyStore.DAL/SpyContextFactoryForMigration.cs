using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpyStore.DAL
{
    public class SpyContextFactoryForMigration : IDesignTimeDbContextFactory<StoreContext>
    {
        private const string ConnectionString = @"Server=LSMINH;Database=SpyStore;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public StoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
            optionsBuilder.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("SpyStore.DAL"));
            return new StoreContext(optionsBuilder.Options);
        }
    }
}
