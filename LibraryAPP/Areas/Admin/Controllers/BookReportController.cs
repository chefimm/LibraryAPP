using LibraryAPP.Models;
using LibraryAPP.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace LibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;
        public BookReportController(ApplicationDbContext context, IToastNotification toast)
        {
            _context = context;
            _toast = toast;
        }
        public async Task<IActionResult> BookReportList()
        {
            ViewBag.BookList = await _context.Book
                .Where(x => x.IsDeleted == false)
                .Where(x => x.KutuphanedeMi == true)
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
            var data = await _context.BookReport
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Book)
                .OrderByDescending(x => x.ToplamAlimSuresi)
                .AsNoTracking()
                .ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> SearchBookReportList(string? q)
        {
            if (!string.IsNullOrEmpty(q))
            {

                var data = await _context.BookReport
                    .Where(x => x.IsDeleted == false)
                    .Where(x =>
                    x.AlanKisiAdSoyad.Contains(q) ||
                    x.AlanKisiTcNo.Contains(q) ||
                    x.AlanKisiTelNo.Contains(q)
                    )
                    .OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .ToListAsync();
                TempData["searchText"] = q;
                return View(data);
            }
            return RedirectToAction("BookReportList");
        }
        public async Task<IActionResult> AddBookReport(int BookId, DateTime AlimTarihi, string AlanKisiAdSoyad, string AlanKisiTcNo, string AlanKisiTelNo)
        {
            BookReport bookReport = new BookReport();
            bookReport.BookId = BookId;
            bookReport.AlimTarihi = AlimTarihi;
            bookReport.AlanKisiAdSoyad = AlanKisiAdSoyad;
            bookReport.AlanKisiTcNo = AlanKisiTcNo;
            bookReport.AlanKisiTelNo = AlanKisiTelNo;
            bookReport.IsDeleted = false;
            bookReport.TeslimTarihi = null;

            var data = await _context.Book.FirstOrDefaultAsync(x => x.Id == BookId);
            data.KutuphanedeMi = false;
            _context.Update(data);
            await _context.AddAsync(bookReport);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Yeni Kayıt Eklendi");
            return RedirectToAction("BookReportList");
        }
        public async Task<IActionResult> UpdateBookReport(int Id, DateTime TeslimTarihi)
        {
            var data = await _context.BookReport.FirstOrDefaultAsync(x => x.Id == Id);
            if (TeslimTarihi < data.AlimTarihi)
            {
                _toast.AddErrorToastMessage("Teslim Tarihi Daha Küçük Olmaz");
                return RedirectToAction("BookReportList");
            }
            data.TeslimTarihi = TeslimTarihi;
            data.ToplamAlimSuresi = (TeslimTarihi.Date - data.AlimTarihi?.Date)?.TotalDays;


            _context.Update(data);
            var BookData = await _context.Book.FirstOrDefaultAsync(x => x.Id == data.BookId);
            BookData.KutuphanedeMi = true;
            _context.Update(BookData);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Kayıt Güncellendi");
            return RedirectToAction("BookReportList");
        }
        public async Task<IActionResult> DeleteBookReport(int id)
        {
            var data = await _context.BookReport.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            data.IsDeleted = true;
            _context.Update(data);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Kayıt Silindi");
            return RedirectToAction("BookReportList");
        }
        [HttpGet]
        public async Task<IActionResult> RecycleListBookReport()
        {
            var data = await _context.BookReport
                .Where(x => x.IsDeleted == true)
                .Include(x => x.Book)
                .OrderByDescending(x => x.ToplamAlimSuresi)
                .AsNoTracking()
                .ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> RecycleBookReport(int id)
        {
            var data = await _context.BookReport.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            data.IsDeleted = false;
            _context.Update(data);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Kayıt Geri Yüklendi");
            return RedirectToAction("RecycleListBookReport");
        }
    }
}
