using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Configuration.BaseModelConfiguration
{
    public class BaseModelConfig<T> : IEntityTypeConfiguration<T> where T : BaseModel
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(42);
            builder.Property(x => x.Description).HasMaxLength(2048);
            builder.Property(x => x.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.UpdatedOn).HasDefaultValueSql("GETDATE()");
        }
    }
}
