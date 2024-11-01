using Microsoft.AspNetCore.Identity;

namespace BookStore.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Address { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
		public DateTime CreatedOn { get; set; } = DateTime.Now;
		public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
