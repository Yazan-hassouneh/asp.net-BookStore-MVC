using BookStore.Repository.BaseRepo;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Repository.AuthorRepo
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        IEnumerable<SelectListItem> GetSelectListItems();
    }
}
