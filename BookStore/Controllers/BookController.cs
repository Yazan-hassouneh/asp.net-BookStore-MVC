using BookStore.Models;
using BookStore.Repository.BaseRepository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController(BaseRepository<Book> bookRepo) : Controller
    {
        
    }
}
