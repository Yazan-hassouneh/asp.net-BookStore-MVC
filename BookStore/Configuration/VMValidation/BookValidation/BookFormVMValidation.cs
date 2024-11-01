namespace BookStore.Configuration.VMValidation.BookValidation
{
	public class BookFormVMValidation<T> : AbstractValidator<T> where T : BookFormVM
	{
        public BookFormVMValidation()
        {
			RuleFor(x => x.PublisherId).NotEmpty().WithMessage("Publisher is required.");
			RuleFor(x => x.CategoriesId).NotEmpty().WithMessage("At Least One Category!.");
			RuleFor(x => x.AuthorsId).NotEmpty().WithMessage("You need At least One Author.");
		}
    }
}
