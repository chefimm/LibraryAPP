using LibraryAPP.Models;
using LibraryAPP.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

namespace LibraryAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToastNotification _toast;
        public BookController(ApplicationDbContext context, IToastNotification toast)
        {
            _context = context;
            _toast = toast;
        }
        public async Task<IActionResult> BookList()
        {
            var data = await _context.Book
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> BookDetail(int id)
        {
            var data = await _context.Book
                .Where(x => x.IsDeleted == false)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return View(data);
        }
        public async Task<IActionResult> SearchBookList(string? q)
        {
            if (!string.IsNullOrEmpty(q))
            {

                var data = await _context.Book
                    .Where(x => x.IsDeleted == false)
                    .Where(x =>
                    x.KitapAdi.Contains(q) ||
                    x.IsbnNo.Contains(q) ||
                    x.YazarAdi.Contains(q)
                    )
                    .OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .ToListAsync();
                TempData["searchText"] = q;
                return View(data);
            }
            return RedirectToAction("BookList");
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book p)
        {
            p.CreateDate = DateTime.Now;
            p.KutuphanedeMi = true;
            await _context.AddAsync(p);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Yeni Kitap Eklendi");
            return RedirectToAction("BookList");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(Book p)
        {
            _context.Update(p);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Kitap Bilgileri Güncellendi");
            return RedirectToAction("BookList");
        }
        public async Task<IActionResult> DeleteBook(int id)
        {
            var data = await _context.Book.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            data.IsDeleted = true;
            _context.Update(data);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Kitap Silindi");
            return RedirectToAction("BookList");
        }
        [HttpGet]
        public async Task<IActionResult> RecycleListBook()
        {
            var data = await _context.Book
                .Where(x => x.IsDeleted == true)
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
            return View(data);
        }
        public async Task<IActionResult> RecycleBook(int id)
        {
            var data = await _context.Book.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            data.IsDeleted = false;
            _context.Update(data);
            await _context.SaveChangesAsync();
            _toast.AddSuccessToastMessage("Kitap Geri Yüklendi");
            return RedirectToAction("RecycleListBook");
        }
    }
}
