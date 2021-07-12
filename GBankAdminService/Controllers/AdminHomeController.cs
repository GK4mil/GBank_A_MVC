using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace GBankAdminService.Controllers
{
    public class AdminHomeController : Controller
    {
        [Authorize(Roles = "superadmin,Admin")]
        public IActionResult Index()
        {
            return View();
        }

    }
}
