using System.ComponentModel.DataAnnotations;

namespace BookStore.CustomAttributes
{
	public class AllowedExtensionAttribute(string allowedExtension) : ValidationAttribute
	{
		private readonly string _allowedExtension = allowedExtension;

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var file = value as IFormFile;

			if (file is not null)
			{
				var extension = Path.GetExtension(file.FileName);
				var isAllowed = _allowedExtension.Split(',').Contains(extension, StringComparer.OrdinalIgnoreCase);

				if (!isAllowed)
				{
					return new ValidationResult($"Only {_allowedExtension} Is Allowed");
				}
			}
			return ValidationResult.Success;
		}
	}
}
