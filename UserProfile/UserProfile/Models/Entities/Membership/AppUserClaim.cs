using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Intro.WebApplicationUI.Models.Entity.Membership
{
    public class AppUserClaim : IdentityUserClaim<int>
    {
    }
}
