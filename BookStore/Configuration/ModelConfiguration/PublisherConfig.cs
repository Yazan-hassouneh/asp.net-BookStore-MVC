using BookStore.Configuration.BaseModelConfiguration;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Configuration.ModelConfiguration
{
    public class PublisherConfig : BaseModelConfig<Publisher>
    {
        public override void Configure(EntityTypeBuilder<Publisher> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Location).HasMaxLength(256);
        }
    }
}
