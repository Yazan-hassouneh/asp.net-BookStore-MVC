using BookStore.Configuration.VMValidation.BaseVMValidation;
using BookStore.VM.PublisherVMs;

namespace BookStore.Configuration.VMValidation.PublisherNMValidation
{
	public class UpdatePublisherVMValidation : BaseVMValidation<UpdatePublisherVM>
	{
        public UpdatePublisherVMValidation() : base()
        {
			RuleFor(x => x.ImagePath).Null().When(x => x.Name == null);
			RuleFor(x => x.Location).NotEmpty().WithMessage("Location Is Required");
		}
    }
}
