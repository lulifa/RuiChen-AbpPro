using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;

namespace RuiChen.AbpPro.Platform
{
    /// <summary>
    /// 最爱菜单管理
    /// </summary>
    [Route("api/platform/menus/favorites")]
    public class UserFavoriteMenuController : AbpProPlatformControllerBase, IUserFavoriteMenuAppService
    {
        protected IUserFavoriteMenuAppService Service { get; }

        public UserFavoriteMenuController(IUserFavoriteMenuAppService service)
        {
            Service = service;
        }

        [HttpPost]
        [Route("{userId}")]
        public virtual Task<UserFavoriteMenuDto> CreateAsync(Guid userId, UserFavoriteMenuCreateDto input)
        {
            return Service.CreateAsync(userId, input);
        }

        [HttpPost]
        [Route("my-favorite-menu")]
        public virtual Task<UserFavoriteMenuDto> CreateMyFavoriteMenuAsync(UserFavoriteMenuCreateDto input)
        {
            return Service.CreateMyFavoriteMenuAsync(input);
        }

        [HttpDelete]
        [Route("{userId}/{MenuId}")]
        public virtual Task DeleteAsync(Guid userId, UserFavoriteMenuRemoveInput input)
        {
            return Service.DeleteAsync(userId, input);
        }

        [HttpDelete]
        [Route("my-favorite-menu/{MenuId}")]
        public virtual Task DeleteMyFavoriteMenuAsync(UserFavoriteMenuRemoveInput input)
        {
            return Service.DeleteMyFavoriteMenuAsync(input);
        }

        [HttpGet]
        [Route("{userId}")]
        public virtual Task<ListResultDto<UserFavoriteMenuDto>> GetListAsync(Guid userId, UserFavoriteMenuGetListInput input)
        {
            return Service.GetListAsync(userId, input);
        }

        [HttpGet]
        [Route("my-favorite-menus")]
        public virtual Task<ListResultDto<UserFavoriteMenuDto>> GetMyFavoriteMenuListAsync(UserFavoriteMenuGetListInput input)
        {
            return Service.GetMyFavoriteMenuListAsync(input);
        }

        [HttpPut]
        [Route("{userId}")]
        public virtual Task<UserFavoriteMenuDto> UpdateAsync(Guid userId, UserFavoriteMenuUpdateDto input)
        {
            return Service.UpdateAsync(userId, input);
        }

        [HttpPut]
        [Route("my-favorite-menu")]
        public virtual Task<UserFavoriteMenuDto> UpdateMyFavoriteMenuAsync(UserFavoriteMenuUpdateDto input)
        {
            return Service.UpdateMyFavoriteMenuAsync(input);
        }
    }
}
