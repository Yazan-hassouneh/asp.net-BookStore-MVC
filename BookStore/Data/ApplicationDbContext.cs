using BookStore.Configuration.ModelConfiguration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Publisher> Publishers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<BookAuthor> BookAuthor { get; set; }
		public DbSet<BookCategory> BookCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
			builder.ApplyConfiguration(new RoleConfig());
			builder.ApplyConfiguration(new BookConfig());
			builder.ApplyConfiguration(new AuthorConfig());
			builder.ApplyConfiguration(new CategoryConfig());
			builder.ApplyConfiguration(new PublisherConfig());
			builder.ApplyConfiguration(new BookAuthorConfig());
			builder.ApplyConfiguration(new BookCategoryConfig());
        }
    }
}
