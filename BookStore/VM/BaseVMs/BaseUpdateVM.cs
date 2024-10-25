namespace BookStore.VM.BaseVMs
{
    public class BaseUpdateVM : BaseAddImageVM
    {
		public string ImagePath { get; set; } = null!;
		public DateTime UpdatedOn { get; set; } = DateTime.Now;
	}
}
