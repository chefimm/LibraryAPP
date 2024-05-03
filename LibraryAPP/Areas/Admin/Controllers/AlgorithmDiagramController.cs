using Microsoft.AspNetCore.Mvc;

namespace LibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AlgorithmDiagramController : Controller
    {
        public IActionResult BookDescription()
        {
            return View();
        }
        public IActionResult BookReportDescription()
        {
            return View();
        }
    }
}
