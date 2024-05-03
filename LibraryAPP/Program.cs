using LibraryAPP.Models.Data;
using Microsoft.EntityFrameworkCore;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealTime")));
builder.Services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
{
    CloseButton = true,
    PositionClass = ToastPositions.TopRight,
    PreventDuplicates = true,
    TimeOut = 4000,
}, new NToastNotifyOption
{
    DefaultSuccessTitle = "BAÞARILI",
    DefaultErrorTitle = "HATA",
    DefaultInfoTitle = "BÝLGÝ",
});
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStatusCodePagesWithReExecute("/hata/{0}");
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=604800");
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Dashboard}/{action=Index}/{id?}");

app.Run();
