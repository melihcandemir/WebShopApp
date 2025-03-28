using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebShopApp.Data.Entities
{
    public class OrderEntity : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }

        // Relation Property
        [ForeignKey("CustomerId")]
        public UserEntity User { get; set; }

        public ICollection<OrderProductEntity> OrderProducts { get; set; }
    }

    public class OrderConfiguration : BaseConfiguration<OrderEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            // burada BaseEntity de bulunan CreatedDate propertsinin ismini OrderDate ile değiştiriyoruz.
            builder.Property(x => x.CreatedDate).HasColumnName("OrderDate");

            base.Configure(builder);
        }
    }
}