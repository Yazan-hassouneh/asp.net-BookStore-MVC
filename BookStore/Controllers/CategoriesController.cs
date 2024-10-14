using BookStore.Models;
using BookStore.Units;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult?> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.Categories.GetAllAsync();

            categories = categories.ToList();

            return View(categories);
        }
    }
}
