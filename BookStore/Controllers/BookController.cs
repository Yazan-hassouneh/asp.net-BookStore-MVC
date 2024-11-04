using BookStore.Configuration.VMValidation.BookValidation;
using BookStore.VM.BookVMs;
using FluentValidation.Results;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageMethods _ImageMethods;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookVM> _createBookValidator;
        private readonly IValidator<UpdateBookVM> _updateBookValidator;
        public BookController(IUnitOfWork unitOfWork, IImageMethods ImageMethods, IMapper mapper, IValidator<CreateBookVM> createBookValidator, IValidator<UpdateBookVM> updateBookValidator)
        {
            _unitOfWork = unitOfWork;
            _unitOfWork = unitOfWork;
            _ImageMethods = ImageMethods;
            _ImageMethods.SetImagePath(FileSettings.BooksImagesPath);
            _mapper = mapper;
			_createBookValidator = createBookValidator;
            _updateBookValidator = updateBookValidator;

		}
        public async Task<ActionResult> Index()
        {
            IEnumerable<Book> books = await _unitOfWork.Books.GetAllAsync();
            List<BookVM> booksVM = [];
            foreach (var book in books.ToList())
            {
                booksVM.Add(_mapper.Map<BookVM>(book));
            }
            return View(booksVM);
        }
        public IActionResult Add()
        {
            var createBook = new CreateBookVM();
            AddSelectListItems(createBook);
            return View(createBook);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateBookVM vm)
        {
            //Validate Vm
            var modelResult = _createBookValidator.Validate(vm);
            if (AddErrorToModelResult(modelResult))
            {
				AddSelectListItems(vm);
				return View(vm);
			}
            string imageName = await _ImageMethods.SaveImage(vm.Image);

            try
            {
                var book = _mapper.Map<Book>(vm);
                book.ImagePath = imageName;
                await _unitOfWork.Books.AddAsync(book);
                await _unitOfWork.Books.Save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                _ImageMethods.DeleteImage(imageName);
                AddErrorToModelResult(modelResult);
                AddSelectListItems(vm);
                return View(vm);
            }

        }
        public async Task<IActionResult> Details(int id)
        {
            Book? book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) return RedirectToAction(nameof(NotFound));

            BookVM bookVM = _mapper.Map<BookVM>(book);
            return View(bookVM);
        }
        public async Task<IActionResult> Update(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) return NotFound();
            var UpdateBookVM = _mapper.Map<UpdateBookVM>(book);
			AddSelectListItems(UpdateBookVM);

			return View(UpdateBookVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateBookVM vm)
        {
            var modelResult = _updateBookValidator.Validate(vm);
			if (AddErrorToModelResult(modelResult))
			{
				AddSelectListItems(vm);
				return View(vm);
			}

			Book? book = await _unitOfWork.Books.FindAsync(x => x.Id == vm.Id);
            if (book == null) BadRequest(ModelState);

            bool hasNewImage = vm.Image is not null;
            string? oldImagePath = book?.ImagePath;
            DateTime createdOn = book?.CreatedOn ?? DateTime.MinValue;

            book = _mapper.Map<Book>(vm);

            if (hasNewImage)
            {
                book.ImagePath = await _ImageMethods.SaveImage(vm.Image!);
            }
            else
            {
                book.ImagePath = oldImagePath;
            }

            _unitOfWork.Books.Update(book);
            int effectedRows = await _unitOfWork.Books.Save();

            if (effectedRows > 0)
            {
                if (hasNewImage) _ImageMethods.DeleteImage(oldImagePath!);
                return RedirectToAction(nameof(Index));
            }

            _ImageMethods.DeleteImage(book.ImagePath);
            return BadRequest(ModelState);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            Book? book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book is null) NotFound();
            _unitOfWork.Books.Delete(book);
            int effectedRows = await _unitOfWork.Books.Save();
            if (effectedRows > 0) _ImageMethods.DeleteImage(book.ImagePath);

            return Ok();
        }
        private bool AddErrorToModelResult(ValidationResult modelResult)
        {
            if (!modelResult.IsValid)
            {
                foreach (var error in modelResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return true;
            }
            return !modelResult.IsValid;
        }
        private void AddSelectListItems(BookFormVM vmForm)
        {
			vmForm.AuthorsList = _unitOfWork.Authors.GetSelectListItems();
			vmForm.PublishersList = _unitOfWork.Publishers.GetSelectListItems();
			vmForm.CategoriesList = _unitOfWork.Categories.GetSelectListItems();
		}
    }
}
