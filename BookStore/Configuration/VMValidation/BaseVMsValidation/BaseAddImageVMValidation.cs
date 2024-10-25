

namespace BookStore.Configuration.VMValidation.BaseVMValidation
{
	public class BaseAddImageVMValidation : BaseVMValidation<BaseAddImageVM>
	{
        public BaseAddImageVMValidation() : base()
        {
            RuleFor(x => x.Image).NotEmpty().WithMessage("Image is required.");
		}
    }
}
