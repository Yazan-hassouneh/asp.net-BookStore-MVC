namespace BookStore.VM.AuthVMs.User
{
	public class ApplicationUserVM : BaseApplicationUserVM
	{
        public bool IsDeleted { get; set; } = false;
	}
}
