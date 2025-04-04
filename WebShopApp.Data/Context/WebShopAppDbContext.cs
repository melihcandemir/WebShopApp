using Microsoft.EntityFrameworkCore;
using WebShopApp.Data.Entities;

namespace WebShopApp.Data.Context
{
    public class WebShopAppDbContext : DbContext
    {
        public WebShopAppDbContext(DbContextOptions<WebShopAppDbContext> options) : base(options)
        {

        }


        // yaptığımı kuralları db ye çağırıyoruz. Fluent Api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProdurctConfiguration());
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());


            modelBuilder.Entity<SettingEntity>().HasData(new SettingEntity
            {
                Id = 1,
                MaintenenceMode = false
            });

            base.OnModelCreating(modelBuilder);
        }


        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<OrderProductEntity> OrderProducts => Set<OrderProductEntity>();
        public DbSet<SettingEntity> Settings { get; set; }
    }
}