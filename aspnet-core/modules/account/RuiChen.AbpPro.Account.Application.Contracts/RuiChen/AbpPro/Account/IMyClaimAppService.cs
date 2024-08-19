using Volo.Abp.Application.Services;

namespace RuiChen.AbpPro.Account
{
    public interface IMyClaimAppService : IApplicationService
    {
        Task ChangeAvatarAsync(ChangeAvatarInput input);
    }

}
