using Client.Domain.Entities;
using Client.Infra.Data.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Client.Infra.Data.Context
{
    public class ClientDbContext : DbContext
    {
        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //Somente adicionar quando for a conexão do banco de dados real
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        }
    }
}