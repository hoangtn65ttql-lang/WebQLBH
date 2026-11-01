using Microsoft.AspNetCore.Mvc;
using WebQLBH.Data;
using WebQLBH.Models;

namespace WebQLBH.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            // Nếu đã đăng nhập rồi thì về trang chủ
            if (HttpContext.Session.GetString("MaNV") != null)
                return RedirectToAction("Index", "Home");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string TenDN, string MatKhau, string returnUrl = null)
        {
            var MaNV = _context.NhanVien_DangNhap(TenDN, MatKhau);
            if (MaNV == 0)
            {
                ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu.");
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }

            var nv = _context.NhanVien_CT(MaNV);

            // Lưu đầy đủ thông tin vào Session
            HttpContext.Session.SetString("MaNV", MaNV.ToString());
            HttpContext.Session.SetString("Username", nv.TenDN ?? "");
            HttpContext.Session.SetString("DisplayName",
                (nv.HoNV + " " + nv.TenNV).Trim());

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DoiMatKhau(string matKhauCu, string matKhauMoi, string xacNhan)
        {
            if (matKhauMoi != xacNhan)
            {
                ViewBag.Error = "Mật khẩu mới và xác nhận không khớp!";
                return View();
            }
            var tenDN = HttpContext.Session.GetString("Username");
            var kq = _context.NhanVien_DoiMatKhau(tenDN!, matKhauCu, matKhauMoi);
            if (kq > 0)
                ViewBag.Success = "Đổi mật khẩu thành công!";
            else
                ViewBag.Error = "Mật khẩu cũ không đúng!";
            return View();
        }
    }
}