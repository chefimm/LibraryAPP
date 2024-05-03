using LibraryAPP.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.KitapSayisi = await _context.Book.Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .CountAsync();
            ViewBag.KayitSayisi = await _context.BookReport.Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .CountAsync();
            ViewBag.TeslimBekleyen = await _context.BookReport.Where(x => x.IsDeleted == false).Where(x => x.TeslimTarihi == null).Include(x => x.Book)
                .OrderBy(x => x.AlimTarihi)
                .AsNoTracking()
                .ToListAsync();
            ViewBag.SonKayitlar = await _context.BookReport.Where(x => x.IsDeleted == false).Include(x => x.Book)
                .OrderByDescending(x => x.ToplamAlimSuresi)
                .AsNoTracking()
                .Take(7)
                .ToListAsync();

            return View();
        }
    }
}
