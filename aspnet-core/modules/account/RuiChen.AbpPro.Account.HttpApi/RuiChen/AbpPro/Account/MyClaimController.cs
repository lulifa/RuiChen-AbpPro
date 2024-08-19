using Microsoft.AspNetCore.Mvc;

namespace RuiChen.AbpPro.Account
{
    /// <summary>
    /// 个人声明
    /// </summary>
    [Route("/api/account/my-claim")]
    public class MyClaimController:AccountControllerBase, IMyClaimAppService
    {
        private readonly IMyClaimAppService myClaimAppService;

        public MyClaimController(IMyClaimAppService myClaimAppService)
        {
            this.myClaimAppService = myClaimAppService;
        }

        /// <summary>
        /// 更换头像
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("change-avatar")]
        public async virtual Task ChangeAvatarAsync(ChangeAvatarInput input)
        {
            await myClaimAppService.ChangeAvatarAsync(input);
        }

    }
}
