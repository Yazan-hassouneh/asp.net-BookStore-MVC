using BookStore.VM.BaseVMs;

namespace BookStore.VM.CategoryVMs
{
    public class CategoryVM : BaseUpdateVM
    {
        public int NumberOfBooksInCategory { get; set; }
    }
}
