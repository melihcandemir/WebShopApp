using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebShopApp.Data.Entities
{
    public class OrderProductEntity : BaseEntity
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quentity { get; set; }

        // Relation Property
        [ForeignKey("OrderId")]
        public OrderEntity Order { get; set; }

        [ForeignKey("ProductId")]
        public ProductEntity Product { get; set; }
    }

    public class OrderProductConfiguration : BaseConfiguration<OrderProductEntity>
    {
        public override void Configure(EntityTypeBuilder<OrderProductEntity> builder)
        {
            // base den gelen Id kolanu kaldırıldı
            builder.Ignore(x => x.Id);
            // Keyler atandı
            builder.HasKey("OrderId", "ProductId");

            base.Configure(builder);
        }
    }
}