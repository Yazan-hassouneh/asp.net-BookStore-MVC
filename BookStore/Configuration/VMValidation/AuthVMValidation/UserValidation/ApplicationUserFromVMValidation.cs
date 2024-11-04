using BookStore.VM.AuthVMs.User;

namespace BookStore.Configuration.VMValidation.AuthVMValidation.UserValidation
{
	public class ApplicationUserFromVMValidation : AbstractValidator<ApplicationUserFormVM>
	{
		public ApplicationUserFromVMValidation()
		{
			RuleFor(x => x.UserName).NotEmpty().WithMessage("Name Is Required").MaximumLength(UserSettings.UserNameMaxLength).WithMessage(UserSettings.UserNameMessageError);
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email Is Required").EmailAddress();
			RuleFor(x => x.PasswordHash)
				.NotEmpty().WithMessage("Password Is Required")
				.MinimumLength(6).WithMessage("Password Must Be At Least 6 Character")
				.Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$ %^&*-]).{6,}$")
				.WithMessage("Must Contains At Least One UpperCase character, One lowercase Character, One Special symbol");
		}
	}
}
