using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using IIdentityUserRepository = RuiChen.AbpPro.Identity.IIdentityUserRepository;

namespace Microsoft.AspNetCore.Identity
{
    /// <summary>
    /// PhoneNumberUserValidator 不会在 ABP vNext 启动时立即执行，而是会在用户管理相关的操作（如用户注册或更新）中被自动调用。
    /// ABP 框架会在执行这些操作时，自动应用所有注册的用户验证器，实现自定义的验证逻辑
    /// 服务注册与替换:将 PhoneNumberUserValidator 注册为 Scoped 生命周期的服务，并且指定它替换任何现有的 IUserValidator<IdentityUser> 实现。
    /// 服务暴露:显示地将 PhoneNumberUserValidator 暴露为 IUserValidator<IdentityUser> 服务，使得依赖注入系统能够在需要该接口的地方使用它
    /// 
    /// 应用场景:
    /// 自定义验证: 在 ABP 框架中，你可能需要自定义用户验证逻辑，比如验证电话号码是否唯一。使用 PhoneNumberUserValidator 替换默认的用户验证器可以
    /// 实现这样的自定义逻辑。服务覆盖: 如果系统中已经存在其他实现 IUserValidator<IdentityUser> 接口的服务，
    /// 通过设置 ReplaceServices = true 可以确保 PhoneNumberUserValidator 被使用，而不是原有的实现。
    /// 
    /// </summary>
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
