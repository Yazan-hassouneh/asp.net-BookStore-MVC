using BookStore.Configuration.VMValidation.BaseVMValidation;

namespace BookStore.Configuration.VMValidation.BookValidation
{
	public class UpdateBookVMValidation : BookFormVMValidation<UpdateBookVM>
	{
        public UpdateBookVMValidation()
        {
			RuleFor(x => x.ImagePath).Null().When(x => x.Name == null);
		}
    }
}
