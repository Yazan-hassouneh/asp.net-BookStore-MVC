using BookStore.Models;
using BookStore.Repository.AuthorRepo;
using BookStore.Repository.BaseRepo;
using BookStore.Repository.BookRepo;
using BookStore.Repository.CategoryRepo;
using BookStore.Repository.PublisherRepo;

namespace BookStore.Units
{
    public interface IUnitOfWork : IDisposable
    {
        public IBookRepository Books { get; }
        public IAuthorRepository Authors { get; }
        public ICategoryRepository Categories { get; }
        public IPublisherRepository Publishers { get; }
        Task<int> Complete();
    }
}
