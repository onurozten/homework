using Microsoft.EntityFrameworkCore;

namespace HomeWork.Entities
{
    public class DataContext : DbContext
    {
        //private string connectionString;

        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryOffDay> CountryOffDays { get; set; }
        public DbSet<BookItem> BookItems { get; set; }


        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}