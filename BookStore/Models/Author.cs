namespace BookStore.Models
{
    public class Author : BaseModel
    {
        public ICollection<BookAuthor> Books { get; set; } = null!;
    }
}
