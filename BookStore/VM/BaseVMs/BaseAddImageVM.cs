using BookStore.CustomAttributes;
using BookStore.Settings;
using System.ComponentModel.DataAnnotations;

namespace BookStore.VM.BaseVMs
{
    public class BaseAddImageVM : BaseVM
    {
        [AllowedExtension(FileSettings.ImagesAllowedExtension)]
        [FileMaxSize(FileSettings.ImagesMaxSizeInBytes)]
        public IFormFile Image { get; set; } = null!;
    }
}
