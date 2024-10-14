using BookStore.Models;
using BookStore.Repository.BaseRepository;

namespace BookStore.Units
{
    public interface IUnitOfWork : IDisposable
    {
        public IBaseRepository<Book> Books { get; }
        public IBaseRepository<Author> Authors { get; }
        public IBaseRepository<Category> Categories { get; }
        public IBaseRepository<Publisher> Publishers { get; }
        Task<int> Complete();
    }
}
