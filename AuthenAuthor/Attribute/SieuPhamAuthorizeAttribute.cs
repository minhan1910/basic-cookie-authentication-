using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthenAuthor.Attribute;

public class SieuPhamAuthorizeAttribute : TypeFilterAttribute<SieuPhamAuthorizeFilter>
{
    public SieuPhamAuthorizeAttribute(string roleName, string actionValue)         
    {
        Arguments = [roleName, actionValue];
    }
}

public class SieuPhamAuthorizeFilter : IAuthorizationFilter
{
    public string RoleName { get; set; }
    public string ActionValue { get; set; }

    public SieuPhamAuthorizeFilter(string roleName, string actionValue)
    {
        RoleName = roleName;
        ActionValue = actionValue;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var httpContext = context.HttpContext;

        if (!CanAccessToAction(httpContext))
            context.Result = new ForbidResult();        
    }

    private bool CanAccessToAction(HttpContext httpContext)
    {
        var role = httpContext.User.FindFirstValue(ClaimTypes.Role);

        if (string.IsNullOrWhiteSpace(role)) return false;

        return role.Equals(RoleName);
    }
}