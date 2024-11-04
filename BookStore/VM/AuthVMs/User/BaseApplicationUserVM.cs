namespace BookStore.VM.AuthVMs.User
{
	public class BaseApplicationUserVM
	{
		public string? Id { get; set; }
		public string UserName { get; set; } = null!;
		public string Email { get; set; } = null!;
        public string? Address { get; set; }
		public List<string> RolesList { get; set; } = [];
	}
}
