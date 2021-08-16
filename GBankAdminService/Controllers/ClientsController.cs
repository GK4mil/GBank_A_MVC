using GBankAdminService.Application.Common.Interfaces;
using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBankAdminService.Controllers
{
    //[Authorize(Roles = "superadmin,Admin")]
    public class ClientsController : Controller
    {
        private readonly GBankDbContext _ct;
        private readonly IUserRepository _ur;
        private readonly IBillRepository _br;
        private readonly IPasswordHashService _phs;

        public ClientsController(GBankDbContext ct, IUserRepository ur, IBillRepository br, IPasswordHashService phs)
        {
            _ct = ct;
            _ur = ur;
            _br = br;
            _phs = phs;
        }
        public async Task<IActionResult> Index()
        {
            
            var res = Task.Run(() => _ct.Users.Where(x => x.ID > 0).ToList());
            
            return View(await res);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User u)
        {
            if (ModelState.IsValid)
            {
                await _ct.Users.AddAsync(u);
                await Task.Run(() => { u.password = _phs.Hash(u.password); });
                await _ct.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
                return View(u);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _ur.DeleteUserByID(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            
            var res = await Task.Run(() => _ct.Users.Where(x => x.ID ==id).ToList().First());
            res.password = default;
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User u)
        {
            var result = await _ur.GetByIdAsync(u.ID);

            if (u.password!=null)
                await Task.Run(() => { result.password = _phs.Hash(u.password); });
            else
            {
                
                result.firstname = u.firstname;
                result.lastname = u.lastname;
                result.active = u.active;
            }
            await _ur.UpdateAsync(result);
            return RedirectToAction("Details", "Clients", new { id = u.ID });
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _ct.Users.Where(x => x.ID == id).Include(c => c.Bills).FirstAsync());
        }
        [HttpGet]
        [ActionName("assign")]
        public async Task<IActionResult> AssignClientToBill(int billid, int clientid)
        {
            var res_bill = await _br.GetByIdAsync(billid);
            var res_client = await _ur.GetByIdAsync(clientid);

            bool ifexists = (await _ct.Bills.Where(x => x.ID == billid).Include(u => u.Users).FirstOrDefaultAsync()).Users.Where(u => u.ID == clientid).ToList().Count == 1 ? true : false;

            if (ifexists)
            {
                TempData["WarningMessage"] = true.ToString();
                TempData["WarningMessageContent"] = "This association already exists!";
                return RedirectToAction("Details", "Clients", new { id = clientid });
            }


            res_bill.Users.Add(res_client);
            await _ct.SaveChangesAsync();
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());

            return RedirectToAction("Details", "Clients", new { id = clientid });
        }

        [HttpGet]        
        public async Task<IActionResult> Search(String q, int billid=0)
        {
            ViewData["billid"] = billid;
            if(q ==null  || q.Equals(""))
                return await Task.Run(() => View(new List<User>()));

            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            //var res = await _ct.Bills.Where(x => x.ID == id).Include(c => c.Users).FirstOrDefaultAsync();
            var res = await _ur.FindByAnyColumn(q);
            return await Task.Run(()=> View(res));
        }

        [HttpGet]
        [ActionName("unassign")]
        public async Task<IActionResult> UnassignClientFromBill(int billid, int clientid)
        {
            var res_bill = await _ct.Bills.Where(i => i.ID == billid).Include(u => u.Users).FirstAsync();
            var res_client = await _ur.GetByIdAsync(clientid);
            
            
            res_bill.Users.Remove(res_client);
            await _ct.SaveChangesAsync();
            

            return RedirectToAction("Details", "Clients", new { id = clientid });
        }

    }
}
