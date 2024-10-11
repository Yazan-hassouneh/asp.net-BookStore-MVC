namespace BookStore.Models
{
    public class Category : BaseModel
    {
        public ICollection<BookCategory> Books { get; set; } = null!;
    }
}
