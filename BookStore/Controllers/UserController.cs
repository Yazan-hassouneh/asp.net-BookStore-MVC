using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
	public class UserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IValidator<ApplicationUserFormVM> userValidator, IMapper mapper) : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager = roleManager;
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly IValidator<ApplicationUserFormVM> _userValidator = userValidator;
		private readonly IMapper _mapper = mapper;

		public async Task<ActionResult> Index()
		{
			var users = await _userManager.Users.ToListAsync();
			if (users is null) return BadRequest("Sorry, Something Went Wrong!!");
			List<ApplicationUserVM> usersVm = [];
            for (int i = 0; i < users.Count; i++)
            {
				var userRoles = await _userManager.GetRolesAsync(users[i]);
				usersVm.Add(_mapper.Map<ApplicationUserVM>(users[i]));
				usersVm[i].RolesList = [.. userRoles];
            }
            return View(usersVm);
		}
		public async Task<ActionResult> Add()
		{
			ApplicationUserFormVM userFormVm = new();
			await AddItemsToSelectList(userFormVm);

            return View(userFormVm);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Add(ApplicationUserFormVM vm)
		{
			try
			{
				var modelResult = _userValidator.Validate(vm);
				if (AddErrorToModelResult(modelResult))
				{
					await AddItemsToSelectList(vm);
					return View(vm);
				}
				ApplicationUser user = _mapper.Map<ApplicationUser>(vm);
				user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, user.PasswordHash);
				if(!result.Succeeded)
				{
                    await AddItemsToSelectList(vm);
					return View(vm);
				}
				await _userManager.AddToRolesAsync(user, vm.RolesList);
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				await AddItemsToSelectList(vm);
				return View(vm);
			}
		}
		public ActionResult Edit(int id)
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
		public ActionResult Details(int id)
		{
			return View();
		}
		public ActionResult Delete(int id)
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
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
		private async Task AddItemsToSelectList(ApplicationUserFormVM vm)
		{
			var roles = await _roleManager.Roles.ToListAsync();
			vm.RolesSelectList = [.. roles.Select(role => new SelectListItem 
			{
				Value = role.Name?.ToString(),
				Text = role.Name
			}).ToList()];
		}
	}
}
