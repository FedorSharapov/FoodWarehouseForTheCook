using FW.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FW.EntityFramework
{
    /// <summary>
    ///  Служебный класс для работы с контекстом и соединением к БД
    /// </summary>
    public class ApplicationContext : DbContext
    {
        DbConnectionOptions _dbConnection;
        public DbSet<Warehouses> Warehouses { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<ChangesProducts> ChangesProducts { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<Recipes> Recipes { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        
        public ApplicationContext(IConfiguration configuration)
        {
            _dbConnection = configuration.GetSection(DbConnectionOptions.KeyValue).Get<DbConnectionOptions>();

            Database.EnsureCreated(); 
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_dbConnection.ConnectionString);
        }
    }
}
