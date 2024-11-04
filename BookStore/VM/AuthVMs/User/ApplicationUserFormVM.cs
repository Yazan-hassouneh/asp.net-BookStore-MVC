using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.VM.AuthVMs.User
{
	public class ApplicationUserFormVM : BaseApplicationUserVM
	{
        public string? PasswordHash { get; set; }
        public string? PhoneNumber { get; set; }
        public IEnumerable<SelectListItem> RolesSelectList { get; set; } = [];
    }
}
