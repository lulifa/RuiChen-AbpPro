using Microsoft.AspNetCore.Mvc;

namespace RuiChen.AbpPro.Admin.HttpApi.Host;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return Redirect("/swagger");
    }
}
