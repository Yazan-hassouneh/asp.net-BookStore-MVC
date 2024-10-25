

using FluentValidation.Results;

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageMethods _ImageMethods;
        private readonly IMapper _mapper;
		private readonly IValidator<CreateCategoryVM> _createCategoryValidator;
		private readonly IValidator<UpdateCategoryVM> _updateCategoryValidator;
		public CategoriesController(IUnitOfWork unitOfWork, IImageMethods ImageMethods, IMapper mapper, IValidator<CreateCategoryVM> createCategoryValidator, IValidator<UpdateCategoryVM> updateCategoryValidator)
        {
            _unitOfWork = unitOfWork;
			_ImageMethods = ImageMethods;
			_ImageMethods.SetImagePath(FileSettings.CategoriesImagesPath);
            _mapper = mapper;
            _createCategoryValidator = createCategoryValidator;
            _updateCategoryValidator = updateCategoryValidator;
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

			if (AddErrorToModelResult(modelResult) || await NameValidate(vm.Name)) return View(vm);

			string imageName = await _ImageMethods.SaveImage(vm.Image);
            try
            {
                var category = _mapper.Map<Category>(vm);
                category.ImagePath = imageName;
                await _unitOfWork.Categories.AddAsync(category);
                await _unitOfWork.Categories.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                AddErrorToModelResult(modelResult);
                _ImageMethods.DeleteImage(imageName);
                return View(vm);
            }
        }
		public async Task<IActionResult> Update(int id)
		{
            var UpdateCategoryVm = await UpdateCategory(id);
            if(UpdateCategoryVm is null) return NotFound();
			return View(UpdateCategoryVm);
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCategoryVM vm)
        {
			var modelResult = _updateCategoryValidator.Validate(vm);

            if (AddErrorToModelResult(modelResult) || await NameValidate(vm.Name))
            {
				var UpdateCategoryVm = await UpdateCategory(vm.Id);
				if (UpdateCategoryVm is null) return NotFound();
				return View(UpdateCategoryVm);
            }

			Category? category = await _unitOfWork.Categories.FindAsync(x => x.Id == vm.Id);
            if (category == null) BadRequest(ModelState);

			bool hasNewImage = vm.Image is not null;
			string? oldImagePath = category?.ImagePath;
            DateTime createdOn = category?.CreatedOn ?? DateTime.MinValue;

            category = _mapper.Map<Category>(vm);
            category.CreatedOn = createdOn;

            if (hasNewImage) 
            {
                category.ImagePath = await _ImageMethods.SaveImage(vm.Image!);
			}else
            {
                category.ImagePath = oldImagePath;
            }

            _unitOfWork.Categories.Update(category);
            int effectedRows = await _unitOfWork.Categories.Save();

            if (effectedRows > 0)
            {
                if(hasNewImage) _ImageMethods.DeleteImage(oldImagePath!);
                return RedirectToAction(nameof(Index));
            } 

            _ImageMethods.DeleteImage(category.ImagePath);
			return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null) NotFound();
            _unitOfWork.Categories.Delete(category);
            int effectedRows = await _unitOfWork.Categories.Save();
            if (effectedRows > 0) _ImageMethods.DeleteImage(category.ImagePath);

            return Ok();
        }
        public async Task<IActionResult?> Details(int id)
        {
            Category? category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
            {
                return RedirectToAction(nameof(NotFound));
            }
            CategoryVM categoryVM = _mapper.Map<CategoryVM>(category);
            return View(categoryVM);
        }
        private bool AddErrorToModelResult(ValidationResult modelResult)
        {
			if (!modelResult.IsValid)
			{
                AddErrorToModelResult(modelResult);
			}
			foreach (var error in modelResult.Errors)
			{
				ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
			}
            return !modelResult.IsValid;
        }
        private async Task<bool> NameValidate(string name)
        {
            bool isExist = await _unitOfWork.Categories.CheckName(name);

            if (isExist)
            {
                ModelState.AddModelError("Name", "The Category Already Exist");
            }
            return isExist;
        }
        private async Task<UpdateCategoryVM> UpdateCategory(int id)
        {
			var category = await _unitOfWork.Categories.GetByIdAsync(id);
			var UpdateCategoryVm = _mapper.Map<UpdateCategoryVM>(category);

            return UpdateCategoryVm;
		}
    }
}
