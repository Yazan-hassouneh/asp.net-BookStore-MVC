using System.ComponentModel.DataAnnotations;

namespace BookStore.CustomAttributes
{
	public class FileMaxSizeAttribute(int maxFileSize) : ValidationAttribute
	{
		private readonly int _maxFileSize = maxFileSize;

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;

			if (file is not null)
			{
				if (file.Length > _maxFileSize)
				{
					return new ValidationResult("File Is Too Large");
				}
			}
			return ValidationResult.Success;
		}
	}
}
