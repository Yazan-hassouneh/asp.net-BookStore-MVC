using BookStore.VM.BaseVMs;

namespace BookStore.VM.CategoryVMs
{
    public class CategoryVM : BaseIncludeImagePathVM
    {
        public int NumberOfBooksInCategory { get; set; }
    }
}
