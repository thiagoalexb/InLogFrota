using InLogFrota.Data.Configurations.Base;
using InLogFrota.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace InLogFrota.Data.Context
{
    public class InLogFrotaContext : DbContext
    {
        public DbSet<Veiculo> Veiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var typesToMapping = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && typeof(IConfiguring).IsAssignableFrom(x));
            
            foreach (var mapping in typesToMapping)
            {
                dynamic mappingClass = Activator.CreateInstance(mapping);
                modelBuilder.ApplyConfiguration(mappingClass);
            }

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=InLogFrota;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }
}
