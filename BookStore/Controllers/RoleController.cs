using BookStore.Data;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
	public class RoleController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, IValidator<RoleFormVM> roleValidator) : Controller
	{
		private readonly ApplicationDbContext _context = context;
		private readonly RoleManager<IdentityRole> _roleManager = roleManager;
		private readonly IValidator<RoleFormVM> _roleValidator = roleValidator;
		public async Task<IActionResult> Index()
		{
			var roles = await _roleManager.Roles.ToListAsync();
			var rolesVm = roles.Select(role => new RoleVM
			{
				Name = role.Name
			}).ToList();
			return View(rolesVm);
		}
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(RoleFormVM vm)
		{
			var modelResult = _roleValidator.Validate(vm);
			if(AddErrorToModelResult(modelResult)) return View(vm);

			await _roleManager.CreateAsync(new IdentityRole(vm.Name));

			return RedirectToAction(nameof(Index));
		}
		private bool AddErrorToModelResult(ValidationResult modelResult)
		{
			if (!modelResult.IsValid)
			{
				foreach (var error in modelResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
				return true;
			}
			return !modelResult.IsValid;
		}
	}
}
