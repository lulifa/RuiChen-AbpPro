# RuiChen.AbpPro.UI.Navigation.VuePureAdmin

适用于 **abp-vue-pure-admin** 的初始化菜单数据模块  

## 配置使用

```csharp
[DependsOn(typeof(AbpUINavigationVuePureAdminModule))]
public class YouProjectModule : AbpModule
{
  // other
}
```

## 配置项

*	AbpUINavigationVuePureAdminOptions.UI				UI名称,默认值: Vue Pure Admin,不建议变更,否则需要改变前端  
*	AbpUINavigationVuePureAdminOptions.LayoutName		布局名称,默认值: Pure Admin Layout,不建议变更,否则需要改变前端  
*	AbpUINavigationVuePureAdminOptions.LayoutPath		布局组件,默认值: LAYOUT,不建议变更,否则需要改变前端  

## 其他
