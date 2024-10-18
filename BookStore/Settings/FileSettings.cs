namespace BookStore.Settings
{
    public static class FileSettings
    {
        public const string CategoriesImagesPath = "/Assets/Img/CategoriesImages/";
        public const string BooksImagesPath = "/Assets/Img/BooksImages/";
        public const string AuthorsImagesPath = "/Assets/Img/AuthorsImages/";
        public const string PublishersImagesPath = "/Assets/Img/PublishersImages/";
        public const string ImagesAllowedExtension = ".jpeg,.jpg,.png";
        public const int ImagesMaxSizeInMB = 1;
        public const int ImagesMaxSizeInBytes = ImagesMaxSizeInMB * 1024 * 1024;
    }
}
