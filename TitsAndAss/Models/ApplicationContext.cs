using Microsoft.EntityFrameworkCore;

namespace TitsAndAss.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public DbSet<Account> accounts{ get; set; }
        public DbSet<Products> products { get; set; }
        public DbSet<Orders> orders { get; set; }

        public DbSet<OrderProducts> userorders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;user=root;password=EnderWarAdmin;database=taa;",
                new MySqlServerVersion(new Version(8, 0, 27))
            );
        }
    }
}
