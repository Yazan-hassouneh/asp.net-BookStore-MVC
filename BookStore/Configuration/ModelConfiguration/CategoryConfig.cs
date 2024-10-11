using BookStore.Configuration.BaseModelConfiguration;
using BookStore.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Configuration.ModelConfiguration
{
    public class CategoryConfig : BaseModelConfig<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

        }
    }
}
