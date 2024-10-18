using BookStore.Settings;
using BookStore.VM.BaseVMs;
using FluentValidation;

namespace BookStore.Configuration.VMValidation.BaseVMValidation
{
	public class BaseVMValidation<T> : AbstractValidator<T> where T : BaseVM
	{
        public BaseVMValidation()
        {
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required !").MaximumLength(BaseModelSettings.MaxNameLength).WithMessage("Name Should Not Be More Than 40 Character");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is Required !").MaximumLength(BaseModelSettings.MaxDescriptionLength).WithMessage("Description Should Not Be More Than 1024 Character");
		}
    }
}
