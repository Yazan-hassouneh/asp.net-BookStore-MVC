

namespace BookStore.Configuration.VMValidation.BaseVMValidation
{
	public class BaseIncludeImagePathVMValidation : BaseVMValidation<BaseIncludeImagePathVM>
	{
		public BaseIncludeImagePathVMValidation() : base()
		{
			RuleFor(x => x.ImagePath).Null().When(x => x.Name == null);
		}
	}
}
