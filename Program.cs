using Microsoft.EntityFrameworkCore;
using WebQLBH.Data;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Đăng ký dịch vụ cho Session ---
builder.Services.AddDistributedMemoryCache(); // Đăng ký bộ nhớ đệm
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session (30 phút)
    options.Cookie.HttpOnly = true; // Bảo mật Cookie
    options.Cookie.IsEssential = true;
});

// Đăng ký dịch vụ để truy cập Session từ trong View (.cshtml)
builder.Services.AddHttpContextAccessor();
// ---------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DA_DbQLBHConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// --- 2. Kích hoạt Middleware Session ---
// Lưu ý: Phải đặt sau UseRouting và TRƯỚC UseAuthorization
app.UseRouting();
app.UseSession();
// ---------------------------------------

app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();