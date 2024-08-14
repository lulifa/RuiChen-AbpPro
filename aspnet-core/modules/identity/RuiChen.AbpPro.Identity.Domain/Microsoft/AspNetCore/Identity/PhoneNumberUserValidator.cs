using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using IIdentityUserRepository = RuiChen.AbpPro.Identity.IIdentityUserRepository;

namespace Microsoft.AspNetCore.Identity
{
    [Dependency(ServiceLifetime.Scoped, ReplaceServices = true)]
    [ExposeServices(typeof(IUserValidator<IdentityUser>))]
    public class PhoneNumberUserValidator : UserValidator<IdentityUser>
    {
        private readonly IIdentityUserRepository userRepository;
        private readonly IStringLocalizer<IdentityResource> stringLocalizer;

        public PhoneNumberUserValidator(IIdentityUserRepository userRepository, IStringLocalizer<IdentityResource> stringLocalizer)
        {
            this.userRepository = userRepository;
            this.stringLocalizer = stringLocalizer;
        }

        public override async Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();
            await ValidatePhoneNumberAsync(manager, user, errors);
            return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : IdentityResult.Success;

            //return errors.Count > 0 ? IdentityResult.Failed(errors.ToArray()) : await base.ValidateAsync(manager, user);
        }

        protected async virtual Task ValidatePhoneNumberAsync(UserManager<IdentityUser> manager, IdentityUser user, ICollection<IdentityError> errors)
        {
            var phoneNumber = await manager.GetPhoneNumberAsync(user);

            if (phoneNumber.IsNullOrWhiteSpace())
            {
                return;
            }

            var owner = await userRepository.FindByPhoneNumberAsync(phoneNumber);

            if (owner != null && !owner.Id.Equals(user.Id))
            {
                errors.Add(new IdentityError
                {
                    Code = "DuplicatePhoneNumber",
                    Description = stringLocalizer["Volo.Abp.Identity:DuplicatePhoneNumber", phoneNumber]
                });

                //throw new UserFriendlyException(stringLocalizer["Volo.Abp.Identity:DuplicatePhoneNumber", phoneNumber]);
            }
        }

    }
}
