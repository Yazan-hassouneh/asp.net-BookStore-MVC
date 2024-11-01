namespace BookStore.VM.BookVMs
{
    public class BookVM : BaseVM
    {
        public string[] Authors { get; set; } = null!;
        public string[] Categories { get; set; } = null!;
        public string PublisherName { get; set; } = null!;
        public int Price { get; set; }
		public short ReleaseYear { get; set; }
		public string ImagePath { get; set; } = null!;
    }
}
