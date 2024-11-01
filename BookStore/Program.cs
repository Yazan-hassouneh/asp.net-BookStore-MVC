using BookStore.Configuration.Mappers;
using BookStore.Data;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookStore.Configuration.VMValidation.CategoriesVMValidation;
using BookStore.Configuration.VMValidation.AuthorVMValidation;
using BookStore.Configuration.VMValidation.PublisherNMValidation;
using BookStore.Configuration.VMValidation.BookValidation;
using BookStore.Configuration.VMValidation.AuthVMValidation.RoleVMValidation;

namespace BookStore
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(connectionString));
			builder.Services.AddDatabaseDeveloperPageExceptionFilter();

			builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddRoles<IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>();
			//builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
			//	.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultUI();
			builder.Services.AddControllersWithViews();

			///	Implement Repository Pattern 
			builder.Services.AddScoped<IImageMethods, ImageMethods>();
			builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
			// Auto mapper
			builder.Services.AddAutoMapper(typeof(CategoryProfile));
			builder.Services.AddAutoMapper(typeof(AuthorProfile));
			builder.Services.AddAutoMapper(typeof(PublisherProfile));
			builder.Services.AddAutoMapper(typeof(BookProfile));
			// Fluent Validation
			builder.Services.AddFluentValidationAutoValidation();
			builder.Services.AddFluentValidationClientsideAdapters();
			builder.Services.AddScoped<IValidator<CreateCategoryVM>, CreateCategoryVMValidation>();
			builder.Services.AddScoped<IValidator<UpdateCategoryVM>, UpdateCategoryVMValidation>();
			builder.Services.AddScoped<IValidator<UpdateAuthorVM>, UpdateAuthorVMValidation>();
			builder.Services.AddScoped<IValidator<CreateAuthorVM>, CreateAuthorVMValidation>();			
			builder.Services.AddScoped<IValidator<UpdatePublisherVM>, UpdatePublisherVMValidation>();
			builder.Services.AddScoped<IValidator<CreatePublisherVM>, CreatePublisherVMValidation>();
			builder.Services.AddScoped<IValidator<CreateBookVM>, CreateBookVMValidation>();
			builder.Services.AddScoped<IValidator<UpdateBookVM>, UpdateBookVMValidation>();
			builder.Services.AddScoped<IValidator<RoleFormVM>, RoleFormVMValidation>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseMigrationsEndPoint();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
			app.MapRazorPages();

			app.Run();
		}
	}
}
