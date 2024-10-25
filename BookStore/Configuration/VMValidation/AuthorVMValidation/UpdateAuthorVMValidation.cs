using BookStore.Configuration.VMValidation.BaseVMValidation;
using BookStore.Configuration.VMValidation.CategoriesVMValidation;

namespace BookStore.Configuration.VMValidation.AuthorVMValidation
{
    public class UpdateAuthorVMValidation : BaseVMValidation<UpdateAuthorVM>
    {
        public UpdateAuthorVMValidation() : base() 
        {
            RuleFor(x => x.ImagePath).Null().When(x => x.Name == null);
        }
    }
}
