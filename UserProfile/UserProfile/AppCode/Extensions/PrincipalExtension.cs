using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UserProfile.AppCode.Extensions
{
    public static partial class Extension
    {

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
            int id = Convert.ToInt32(identity.Claims.FirstOrDefault(i => i.Type.Equals(ClaimTypes.NameIdentifier)).Value);
            return id;
        }
        public static int GetUserId(this IActionContextAccessor ctx)
        {
            int id = ctx.ActionContext.HttpContext.User.GetUserId();
            return id;
        }
    }
}
