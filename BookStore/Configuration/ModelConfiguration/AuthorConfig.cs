using BookStore.Configuration.BaseModelConfiguration;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Configuration.ModelConfiguration
{
    public class AuthorConfig : BaseModelConfig<Author>
    {
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            base.Configure(builder);
        }
    }
}
