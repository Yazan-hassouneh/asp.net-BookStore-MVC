using BookStore.Models;
using BookStore.Repository.BaseRepo;
using BookStore.Repository.CategoryRepo;
using BookStore.Repository.PublisherRepo;

namespace BookStore.Units
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Book> Books { get; }
        public IBaseRepository<Author> Authors { get; }
        public ICategoryRepository Categories { get; }
        public IPublisherRepository Publishers { get; }
        Task<int> Complete();
    }
}
