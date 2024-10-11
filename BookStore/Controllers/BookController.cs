using BookStore.Models;
using BookStore.Repository.BaseRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController(BaseRepository<Book> bookRepo) : Controller
    {
        private readonly BaseRepository<Book> _bookRepo = bookRepo;

        public async Task<Book?> Index()
        {
            return await _bookRepo.GetById(1);
        }
    }
}
