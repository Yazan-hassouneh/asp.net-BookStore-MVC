

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageMethods _categoryImageMethods;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnv;
		private readonly IValidator<CreateCategoryVM> _createCategoryValidator;
		public CategoriesController(IUnitOfWork unitOfWork, IImageMethods categoryImageMethods, IMapper mapper, IValidator<CreateCategoryVM> createCategoryValidator)
        {
            _unitOfWork = unitOfWork;
            _categoryImageMethods = categoryImageMethods;
			_categoryImageMethods.SetImagePath(FileSettings.CategoriesImagesPath);
            _mapper = mapper;
            //_webHostEnv = webHostEnv;
            _createCategoryValidator = createCategoryValidator;
		}
        public async Task<IActionResult?> Index()
        {
            IEnumerable<Category> categories = await _unitOfWork.Categories.GetAllAsync();

            List<CategoryVM> categoriesVm = [];

            foreach (var category in categories.ToList())
            {
                categoriesVm.Add(_mapper.Map<CategoryVM>(category));
            }

            return View(categoriesVm);
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
        public async Task<IActionResult?> Details(int id)
        {
            Category category = await _unitOfWork.Categories.GetByIdAsync(id);
            if(category is null) RedirectToAction(nameof(NotFound));
            CategoryVM categoryVM = _mapper.Map<CategoryVM>(category);
            return View(categoryVM);
        }
    }
}
