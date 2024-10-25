using BookStore.Repository.BaseRepo;

namespace BookStore.Repository.CategoryRepo
{
	public interface ICategoryRepository : IBaseRepository<Category>
	{
		Task<bool> CheckName(string name);
	}
}
