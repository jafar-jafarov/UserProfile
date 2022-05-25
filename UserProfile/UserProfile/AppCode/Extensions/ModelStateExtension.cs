using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserProfile.AppCode.Extensions
{
    public static partial class Extension
    {
        public static bool IsValid(this IActionContextAccessor ctx)
        {
            return ctx.ActionContext.ModelState.IsValid;
        }

        public static IActionContextAccessor AddModelError(this IActionContextAccessor ctx, string key, string message)
        {
            ctx.ActionContext.ModelState.AddModelError(key, message);

            return ctx;
        }
    }
}
