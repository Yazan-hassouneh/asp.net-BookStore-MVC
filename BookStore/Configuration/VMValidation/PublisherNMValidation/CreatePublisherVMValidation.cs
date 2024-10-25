using BookStore.Configuration.VMValidation.BaseVMValidation;
using BookStore.VM.PublisherVMs;

namespace BookStore.Configuration.VMValidation.PublisherNMValidation
{
	public class CreatePublisherVMValidation : BaseAddImageVMValidation
	{
		public CreatePublisherVMValidation() : base()
		{
			RuleFor(x => ((CreatePublisherVM)x).Location).NotEmpty().WithMessage("Location is required.");
		}

	}
}
