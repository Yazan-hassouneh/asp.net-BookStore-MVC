using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Configuration.ModelConfiguration
{
	public class UserConfig : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(x => x.UserName).IsRequired().HasMaxLength(UserSettings.UserNameMaxLength);
			builder.Property(x => x.Email).IsRequired();
			builder.Property(x => x.PasswordHash).IsRequired();
		}
	}
}
