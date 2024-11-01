using BookStore.Data;
using BookStore.Repository.BaseRepo;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository.AuthorRepo
{
    public class AuthorRepository(ApplicationDbContext context) : BaseRepository<Author>(context) , IAuthorRepository
    {
        private readonly ApplicationDbContext _context = context;

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return [.. _context.Authors.Select(author => new SelectListItem
            {
                Value = author.Id.ToString(),
                Text = author.Name,
            }).OrderBy(d => d.Text).AsNoTracking()];
        }
    }
}
