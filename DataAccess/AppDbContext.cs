namespace DataAccess
{
    using DataAccess.Entities;
    using DataAccess.EntityBuilders;
    using Microsoft.EntityFrameworkCore;

    public  class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<CardEntity> Cards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FileEntityTypeConfiguration());
        }
    }
}
