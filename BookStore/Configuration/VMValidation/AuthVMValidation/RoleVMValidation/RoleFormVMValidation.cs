namespace BookStore.Configuration.VMValidation.AuthVMValidation.RoleVMValidation
{
    public class RoleFormVMValidation : AbstractValidator<RoleFormVM>
    {
        public RoleFormVMValidation()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name Is Required").MaximumLength(RoleSettings.RoleNameMaxLength).WithMessage(RoleSettings.RoleNameMessageError);
        }
    }
}
