using AutoMapper;
using BookStore.Files;
using BookStore.Models;
using BookStore.Settings;
using BookStore.Units;
using BookStore.VM.CategoryVMs;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageMethods _categoryImageMethods;
        private readonly IMapper _mapper;
		private readonly IValidator<CategoryVM> _categoryValidator;
		private readonly IValidator<CreateCategoryVM> _createCategoryValidator;
		public CategoriesController(IUnitOfWork unitOfWork, IImageMethods categoryImageMethods, IMapper mapper, IValidator<CategoryVM> categoryValidator, IValidator<CreateCategoryVM> createCategoryValidator)
        {
            _unitOfWork = unitOfWork;
            _categoryImageMethods = categoryImageMethods;
			_categoryImageMethods.SetImagePath(FileSettings.CategoriesImagesPath);
            _mapper = mapper;
            _categoryValidator = categoryValidator;
            _createCategoryValidator = createCategoryValidator;
		}
        public async Task<IActionResult?> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.Categories.GetAllAsync();

            categories = categories.ToList();

            return View(categories);
        }
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateCategoryVM vm)
        {
            //Validate Vm
            var modelResult = _createCategoryValidator.Validate(vm);
            if(!modelResult.IsValid)
            {
				foreach (var error in modelResult.Errors)
				{
					ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
				}
                return View(vm);
			}
            //Save Image
            string imageName = await _categoryImageMethods.SaveImage(vm.Image);
            //mapping
            var category = _mapper.Map<Category>(vm);
            category.ImagePath = imageName;
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.Categories.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id)
        {
            return View();
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}
