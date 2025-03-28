using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebShopApp.Data.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    // burda TEntity = BaseEntity ve ondan miras alan entityler
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        // Genel olarak verilen kurallar
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Bütün sorgulamalar ve linq işlemlerinde IsDeleted == false olanlar çağrılır.
            builder.HasQueryFilter(x => x.IsDeleted == false);

            // ModifiedDate == null olabilir. Doldurulmak zorunda değildir.
            builder.Property(x => x.ModifiedDate).IsRequired(false);
        }
    }
}