using Microsoft.AspNetCore.Mvc;

namespace LibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsedtechnologiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
