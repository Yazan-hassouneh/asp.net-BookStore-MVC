using BookStore.Configuration.VMValidation.BaseVMValidation;

namespace BookStore.Configuration.VMValidation.CategoriesVMValidation
{
	public class UpdateCategoryVMValidation : AbstractValidator<UpdateCategoryVM>
	{
		public UpdateCategoryVMValidation() : base()
		{
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required !").MaximumLength(BaseModelSettings.MaxNameLength).WithMessage("Name Should Not Be More Than 40 Character");
			RuleFor(x => x.Description).NotEmpty().WithMessage("Description Is Required !").MaximumLength(BaseModelSettings.MaxDescriptionLength).WithMessage("Description Should Not Be More Than 1024 Character");
			RuleFor(x => x.ImagePath).Null().When(x => x.Name == null);
		}
	}
}
