using BookStore.Configuration.BaseModelConfiguration;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Configuration.ModelConfiguration
{
    public class BookConfig : BaseModelConfig<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);
            builder.ToTable(x => x.HasCheckConstraint("Price", "Price >= 1"));
        }
    }
}
