namespace BookStore.VM.PublisherVMs
{
    public class PublisherVM : BaseUpdateVM
    {
        public string Location { get; set; } = null!;
        public int NumberOfBooks { get; set; }
    }
}
