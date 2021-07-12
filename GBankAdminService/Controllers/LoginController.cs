using AutoMapper;
using GBankAdminService.Application.Functions.Login.Query;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace GBankAdminService.Controllers
{
    public class LoginController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _autoMapper;

        public LoginController(IMediator mediator, IMapper autoMapper)
        {      
            _mediator = mediator;
            _autoMapper = autoMapper;
        }
        public IActionResult Index()
        {
            return View("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> TryToLoginAsync(GBankAdminService.Models.CredentialsModel model)
        {
            ClaimsIdentity identity = null;
            SearchUserToLoginQuery q = _autoMapper.Map<SearchUserToLoginQuery>(model);
            var result =  _mediator.Send(q);
            
            if((await result).Count==1)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, (await result).FirstOrDefault().Username),
                    new Claim(ClaimTypes.Role, "superadmin")
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                
                


                return RedirectToAction("Index","AdminHome");
            }
            else
            {
                HttpContext.Session.Remove("is_logged");
                return View("Index");
            }
            
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return View("Index");
        }

    }
}
