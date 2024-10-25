using BookStore.VM.PublisherVMs;
using FluentValidation.Results;

namespace BookStore.Controllers
{
	public class PublisherController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IImageMethods _ImageMethods;
		private readonly IMapper _mapper;
		private readonly IValidator<CreatePublisherVM> _createPublisherValidator;
		private readonly IValidator<UpdatePublisherVM> _updatePublisherValidator;
		public PublisherController(IUnitOfWork unitOfWork, IImageMethods ImageMethods, IMapper mapper, IValidator<CreatePublisherVM> createPublisherValidator, IValidator<UpdatePublisherVM> updatePublisherValidator)
		{
			_unitOfWork = unitOfWork;
			_ImageMethods = ImageMethods;
			_ImageMethods.SetImagePath(FileSettings.PublishersImagesPath);
			_mapper = mapper;
			_createPublisherValidator = createPublisherValidator;
			_updatePublisherValidator = updatePublisherValidator;
		}
		public async Task<IActionResult?> Index()
		{
			IEnumerable<Publisher> publishers = await _unitOfWork.Publishers.GetAllAsync();

			List<PublisherVM> publisherVm = [];

			foreach (var publisher in publishers.ToList())
			{
				publisherVm.Add(_mapper.Map<PublisherVM>(publisher));
			}

			return View(publisherVm);
		}
		public IActionResult Add()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Add(CreatePublisherVM vm)
		{
			//Validate Vm
			var modelResult = _createPublisherValidator.Validate(vm);

			if (AddErrorToModelResult(modelResult) || await NameValidate(vm.Name)) return View(vm);

			string imageName = await _ImageMethods.SaveImage(vm.Image);
			try
			{
				var publisher = _mapper.Map<Publisher>(vm);
				publisher.ImagePath = imageName;
				await _unitOfWork.Publishers.AddAsync(publisher);
				await _unitOfWork.Publishers.Save();
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
			var UpdatePublisherVm = await UpdatePublisher(id);
			if (UpdatePublisherVm is null) return NotFound();
			return View(UpdatePublisherVm);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(UpdatePublisherVM vm)
		{
			var modelResult = _updatePublisherValidator.Validate(vm);

			if (AddErrorToModelResult(modelResult) || await NameValidate(vm.Name))
			{
				var UpdatePublisherVm = await UpdatePublisher(vm.Id);
				if (UpdatePublisherVm is null) return NotFound();
				return View(UpdatePublisherVm);
			}

			Publisher? publisher = await _unitOfWork.Publishers.FindAsync(x => x.Id == vm.Id);
			if (publisher == null) BadRequest(ModelState);

			bool hasNewImage = vm.Image is not null;
			string? oldImagePath = publisher?.ImagePath;
			DateTime createdOn = publisher?.CreatedOn ?? DateTime.MinValue;

			publisher = _mapper.Map<Publisher>(vm);
			publisher.CreatedOn = createdOn;

			if (hasNewImage)
			{
				publisher.ImagePath = await _ImageMethods.SaveImage(vm.Image!);
			}
			else
			{
				publisher.ImagePath = oldImagePath;
			}

			_unitOfWork.Publishers.Update(publisher);
			int effectedRows = await _unitOfWork.Publishers.Save();

			if (effectedRows > 0)
			{
				if (hasNewImage) _ImageMethods.DeleteImage(oldImagePath!);
				return RedirectToAction(nameof(Index));
			}

			_ImageMethods.DeleteImage(publisher.ImagePath);
			return BadRequest(ModelState);
		}
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			Publisher? publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
			if (publisher is null) NotFound();
			_unitOfWork.Publishers.Delete(publisher);
			int effectedRows = await _unitOfWork.Publishers.Save();
			if (effectedRows > 0) _ImageMethods.DeleteImage(publisher.ImagePath);

			return Ok();
		}
		public async Task<IActionResult?> Details(int id)
		{
			Publisher? publisher = await _unitOfWork.Publishers.GetByIdAsync(id);
			if (publisher is null) return RedirectToAction(nameof(NotFound));
			PublisherVM publisherVM = _mapper.Map<PublisherVM>(publisher);
			return View(publisherVM);
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
			bool isExist = await _unitOfWork.Publishers.CheckName(name);

			if (isExist)
			{
				ModelState.AddModelError("Name", "The Publisher Already Exist");
			}
			return isExist;
		}
		private async Task<UpdatePublisherVM> UpdatePublisher(int id)
		{
			var publishers = await _unitOfWork.Publishers.GetByIdAsync(id);
			var UpdatePublisherVm = _mapper.Map<UpdatePublisherVM>(publishers);

			return UpdatePublisherVm;
		}
	}
}
