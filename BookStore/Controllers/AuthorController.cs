using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
	public class AuthorController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IImageMethods _ImageMethods;
		private readonly IMapper _mapper;
		private readonly IValidator<CreateAuthorVM> _createAuthorValidator;
		private readonly IValidator<UpdateAuthorVM> _updateAuthorValidator;
		public AuthorController(IUnitOfWork unitOfWork, IImageMethods ImageMethods, IMapper mapper, IValidator<CreateAuthorVM> createAuthorValidator, IValidator<UpdateAuthorVM> updateAuthorValidator)
        {
            _unitOfWork = unitOfWork;
			_unitOfWork = unitOfWork;
			_ImageMethods = ImageMethods;
			_ImageMethods.SetImagePath(FileSettings.AuthorsImagesPath);
			_mapper = mapper;
            _createAuthorValidator = createAuthorValidator;
            _updateAuthorValidator = updateAuthorValidator;
		}
        public async Task<ActionResult> Index()
		{
			IEnumerable<Author> authors = await  _unitOfWork.Authors.GetAllAsync();
			List<AuthorVM> authorsVM = [];
            foreach (var author in authors.ToList())
            {
				authorsVM.Add(_mapper.Map<AuthorVM>(author));
            }
            return View(authorsVM);
		}
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(CreateAuthorVM vm)
		{
            //Validate Vm
            var modelResult = _createAuthorValidator.Validate(vm);

            if (AddErrorToModelResult(modelResult)) return View(vm);

            string imageName = await _ImageMethods.SaveImage(vm.Image);
			try
			{
                var author = _mapper.Map<Author>(vm);
                author.ImagePath = imageName;
                await _unitOfWork.Authors.AddAsync(author);
                await _unitOfWork.Authors.Save();
                return RedirectToAction(nameof(Index));
            }
			catch
			{
				_ImageMethods.DeleteImage(imageName);
				AddErrorToModelResult(modelResult);
                return View(vm);
			}

		}
		public async Task<IActionResult> Details(int id)
		{
			Author? author = await _unitOfWork.Authors.GetByIdAsync(id);
			if (author is null)
			{
				return RedirectToAction(nameof(NotFound));
			}
			AuthorVM authorVM = _mapper.Map<AuthorVM>(author);
			return View(authorVM);
		}
		public async Task<IActionResult> Update(int id)
		{
			var author = await _unitOfWork.Authors.GetByIdAsync(id);
			if (author is null) return NotFound();
			var UpdateAuthorVm = _mapper.Map<UpdateAuthorVM>(author);
			return View(UpdateAuthorVm);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(UpdateAuthorVM vm)
		{
			var modelResult = _updateAuthorValidator.Validate(vm);
			if (AddErrorToModelResult(modelResult)) return View(vm);

			Author? author = await _unitOfWork.Authors.FindAsync(x => x.Id == vm.Id);
			if (author == null) BadRequest(ModelState);

			bool hasNewImage = vm.Image is not null;
			string? oldImagePath = author?.ImagePath;
			DateTime createdOn = author?.CreatedOn ?? DateTime.MinValue;

			author = _mapper.Map<Author>(vm);
			author.CreatedOn = createdOn;

			if (hasNewImage)
			{
				author.ImagePath = await _ImageMethods.SaveImage(vm.Image!);
			}
			else
			{
				author.ImagePath = oldImagePath;
			}

			_unitOfWork.Authors.Update(author);
			int effectedRows = await _unitOfWork.Categories.Save();

			if (effectedRows > 0)
			{
				if (hasNewImage) _ImageMethods.DeleteImage(oldImagePath!);
				return RedirectToAction(nameof(Index));
			}

			_ImageMethods.DeleteImage(author.ImagePath);
			return BadRequest(ModelState);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			Author? author = await _unitOfWork.Authors.GetByIdAsync(id);
			if (author is null) NotFound();
			_unitOfWork.Authors.Delete(author);
			int effectedRows = await _unitOfWork.Authors.Save();
			if (effectedRows > 0) _ImageMethods.DeleteImage(author.ImagePath);

			return Ok();
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
    }
}
