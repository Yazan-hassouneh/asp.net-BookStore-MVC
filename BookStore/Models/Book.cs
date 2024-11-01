namespace BookStore.Models
{
    public class Book : BaseModel
    {
        public int Price { get; set; }
        public int PublisherId { get; set; }
        public short ReleaseYear { get; set; }
        public Publisher Publisher { get; set; } = null!;
        public ICollection<BookAuthor> Authors { get; set; } = null!;
        public ICollection<BookCategory> Categories { get; set; } = null!;

    }
}
