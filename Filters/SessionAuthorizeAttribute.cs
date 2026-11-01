using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionAuthorize : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        // Kiểm tra xem Session "AdminLogin" có tồn tại không
        var session = filterContext.HttpContext.Session.GetString("AdminLogin");

        if (string.IsNullOrEmpty(session))
        {
            // Nếu không có, đá về trang Login
            filterContext.Result = new RedirectToActionResult("Login", "Account", null);
        }

        base.OnActionExecuting(filterContext);
    }
}