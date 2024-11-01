using BookStore.Configuration.VMValidation.BaseVMValidation;

namespace BookStore.Configuration.VMValidation.BookValidation
{
	public class CreateBookVMValidation : BookFormVMValidation<CreateBookVM>
	{
        public CreateBookVMValidation() : base()
        {
			RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required.");
		}
    }
}
