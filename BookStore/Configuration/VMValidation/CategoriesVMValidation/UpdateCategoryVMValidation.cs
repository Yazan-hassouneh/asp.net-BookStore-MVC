using BookStore.Configuration.VMValidation.BaseVMValidation;

namespace BookStore.Configuration.VMValidation.CategoriesVMValidation
{
	public class UpdateCategoryVMValidation : BaseVMValidation<UpdateCategoryVM>
	{
		public UpdateCategoryVMValidation() : base()
		{
			RuleFor(x => x.ImagePath).Null().When(x => x.Name == null);
		}
	}
}
