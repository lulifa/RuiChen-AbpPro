using Volo.Abp.Auditing;
using Volo.Abp.ObjectExtending;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.Validation;

namespace RuiChen.AbpPro.OpenIddict
{
    public abstract class OpenIddictApplicationCreateOrUpdateDto : ExtensibleObject
    {
        /// <summary>
        /// 客户端秘钥
        /// </summary>
        [DisableAuditing]
        public string ClientSecret { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        [DynamicStringLength(typeof(OpenIddictApplicationConsts),nameof(OpenIddictApplicationConsts.ClientTypeMaxLength))]
        public string ClientType { get; set; }

        /// <summary>
        /// 客户端应用程序的主页 URI。通常用于描述客户端应用程序的位置
        /// </summary>
        public string ClientUri { get; set; }

        /// <summary>
        /// 定义了用户授权的类型。可能的值包括 Implicit（隐式授权）或 Explicit（明确授权），指示用户是否需要显式同意
        /// </summary>
        public string ConsentType { get; set; }

        /// <summary>
        /// 客户端应用程序的显示名称，用于描述应用程序，通常在用户界面上显示
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 客户端应用程序的多语言显示名称。可以为不同语言设置不同的显示名称
        /// </summary>
        public Dictionary<string, string> DisplayNames { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 用于配置 OAuth 2.0 和 OpenID Connect 端点的集合，如授权端点、令牌端点等
        /// </summary>
        public List<string> Endpoints { get; set; } = new List<string>();

        /// <summary>
        /// 指定客户端应用程序支持的授权类型（授权码、隐式、客户端凭据等），这些类型定义了如何获得访问令牌
        /// </summary>
        public List<string> GrantTypes { get; set; } = new List<string>();

        /// <summary>
        /// 定义客户端应用程序支持的响应类型，如 code（授权码），token（令牌）等，影响授权服务器返回的内容
        /// </summary>
        public List<string> ResponseTypes { get; set; } = new List<string>();

        /// <summary>
        /// 客户端应用程序请求的权限范围。例如，openid、profile、email 等
        /// </summary>
        public List<string> Scopes { get; set; } = new List<string>();

        /// <summary>
        /// 用户注销后重定向的 URI 列表。当用户退出登录后，应用程序会将用户重定向到这些 URI
        /// </summary>
        public List<string> PostLogoutRedirectUris { get; set; } = new List<string>();

        /// <summary>
        /// 用于存储客户端应用程序的自定义属性。这些属性可以是任何键值对，用于扩展或特定的应用需求
        /// </summary>
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 授权服务器重定向用户到客户端应用程序的 URI 列表。这些 URI 必须是预先注册的，用于处理授权响应
        /// </summary>
        public List<string> RedirectUris { get; set; } = new List<string>();

        /// <summary>
        /// 定义客户端应用程序的要求或限制。例如，是否需要进行客户端身份验证，或者是否有特殊的安全要求
        /// </summary>
        public List<string> Requirements { get; set; } = new List<string>();

        /// <summary>
        /// 指定客户端应用程序的类型。常见类型包括 web（Web 应用程序）、native（原生应用程序）、spa（单页应用程序）等
        /// </summary>
        [DynamicStringLength(typeof(OpenIddictApplicationConsts), nameof(OpenIddictApplicationConsts.ApplicationTypeMaxLength))]
        public string ApplicationType { get; set; }

        /// <summary>
        /// 客户端应用程序的图标 URI。用于提供一个图标，以便在授权服务器的用户界面上显示
        /// </summary>
        public string LogoUri { get; set; }
    }
}
