namespace BookStore.Models
{
    public class Publisher : BaseModel
    {
        public string Location { get; set; } = null!;
        public ICollection<Book> Books { get; set; } = null!;
    }
}
