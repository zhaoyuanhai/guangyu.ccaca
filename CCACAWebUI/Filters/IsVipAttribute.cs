using CCACAWebUI.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace CCACAWebUI.Filters
{
    public class IsVipAttribute : Attribute, IActionFilter
    {

        public IsVipAttribute()
            : base()
        { }

        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                using (var db = new DbEntityContext())
                {
                    var idClaims = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "id");
                    var id = int.Parse(idClaims.Value);
                    var user = db.Users.First(x => x.ID == id);
                    if (!user.IsVip)
                    {
                        context.Result = new RedirectResult("/NoAuthority");
                    }
                }
            }
        }
    }
}
