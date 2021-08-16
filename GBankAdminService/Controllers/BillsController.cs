using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Application.Functions.Bills.Command;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using MediatR;
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
    public class BillsController : Controller
    {
        private readonly IBillRepository _br;
        private readonly IUserRepository _ur;
        private readonly IMediator _m;
        private readonly GBankDbContext _ct;

        public BillsController(IBillRepository br, GBankDbContext ct, IUserRepository ur, IMediator m)
        {
            _br = br;
            _ct = ct;
            _ur = ur;
            _m = m;
        }
        public async Task<IActionResult> Index()
        {
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            return View(await _ct.Bills.Where(x=>x.ID>0).Include(u=>u.Users).ToListAsync());
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewData["id"] = id;
            return View(await _ct.Bills.Where(x => x.ID == id).Include(c=>c.Users).FirstAsync());
            //return View(await _ct.Bills.Where(x => x.ID == id).Include(c => c.User).FirstAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Bill b, int id)
        {
            
            var res = await ( _ct.Bills.Where(x => x.ID == id).FirstOrDefaultAsync());
            res.balance = b.balance;
            await _ct.SaveChangesAsync();



            return RedirectToAction("Details", "Bills", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            ViewData["id"] = id;          
            return await Task.Run(() => View("Create"));
        }

        [HttpPost]
        public async Task<IActionResult> Create(Bill b, int id)
        {
            b.ID = default;
            b = await _m.Send(new AddBillCommand() { balance = b.balance.ToString() });
            int billid = b.ID;

            if (id == 0)
            {       
                return await Task.Run(() => RedirectToAction("Details", "Bills", new { id = billid }));
            }
            else
            {
                var user = await _ur.GetByIdAsync(id);
                user.Bills.Add(b);
                await _ct.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Details", "Clients", new { id = id }));
            }
        }


        [TempData]
        public string WarningMessage { get; set; }


        public async Task<IActionResult> Details(int id)
        {
            var res = await _ct.Bills.Where(x => x.ID == id).Include(c => c.Users).FirstOrDefaultAsync();
            return View(res);
        }
       
        


        [HttpGet]
         public async Task<IActionResult> Delete(int id)
         {

            await _br.DeleteByIdAsync(id);
             
             return await Task.Run(() => RedirectToAction("Index"));
         }

        [HttpGet]
        public async Task<IActionResult> Search(String q, int clientid = 0)
        {
            ViewData["clientid"] = clientid;
            if (q == null || q.Equals(""))
                return await Task.Run(() => View(new List<Bill>()));

            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            //var res = await _ct.Bills.Where(x => x.ID == id).Include(c => c.Users).FirstOrDefaultAsync();
            var res = await _br.FindByAnyColumn(q);
            return await Task.Run(() => View(res));
        }

        [HttpGet]
        [ActionName("assign")]
        public async Task<IActionResult> AssignClientToBill(int billid, int clientid)
        {
            var res_bill = await _br.GetByIdAsync(billid);
            var res_client = await _ur.GetByIdAsync(clientid);

            bool ifexists = (await _ct.Bills.Where(x => x.ID == billid).Include(u => u.Users).FirstOrDefaultAsync()).Users.Where(u => u.ID == clientid).ToList().Count == 1 ? true: false ;

            if (ifexists)
            {
                TempData["WarningMessage"] = true.ToString();
                TempData["WarningMessageContent"] = "This association already exists!";
                return RedirectToAction("Details", "Bills", new { id = billid });
            }
   
            res_bill.Users.Add(res_client);
            await _ct.SaveChangesAsync();
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());

            return RedirectToAction("Details", "Bills", new { id = billid });
        }

        [HttpGet]
        [ActionName("unassign")]
        public async Task<IActionResult> UnassignBillFromClient(int billid, int clientid)
        {
            var res_bill = await _ct.Bills.Where(i => i.ID == billid).Include(u => u.Users).FirstAsync();
            var res_client = await _ur.GetByIdAsync(clientid);


            res_bill.Users.Remove(res_client);
            await _ct.SaveChangesAsync();


            return RedirectToAction("Details", "Bills", new { id = billid });
        }

    }
}
