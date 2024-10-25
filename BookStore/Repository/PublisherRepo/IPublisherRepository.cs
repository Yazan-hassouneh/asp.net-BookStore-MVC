using BookStore.Repository.BaseRepo;

namespace BookStore.Repository.PublisherRepo
{
	public interface IPublisherRepository : IBaseRepository<Publisher>
	{
		Task<bool> CheckName(string name);
	}
}
