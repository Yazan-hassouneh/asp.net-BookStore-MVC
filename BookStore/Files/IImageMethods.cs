namespace BookStore.Files
{
	public interface IImageMethods
	{
		public void SetImagePath(string path);
		public Task<string> SaveImage(IFormFile image);
		public void DeleteImage(string imageName);
	}
}
