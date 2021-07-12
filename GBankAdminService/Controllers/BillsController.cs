using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GBankAdminService.Controllers
{
    public class BillsController : Controller
    {
        private readonly IBillRepository _br;
        private readonly IUserRepository _ur;
        private readonly GBankDbContext _ct;

        public BillsController(IBillRepository br, GBankDbContext ct, IUserRepository ur)
        {
            _br = br;
            _ct = ct;
            _ur = ur;
        }
        public async Task<IActionResult> Index()
        {
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            return View(await _br.GetAllAsync());
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

            
            
            return RedirectToAction("Index");
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
            int billid = (await _br.AddAsync(b)).ID;
            if (id == 0)
            {       
                return await Task.Run(() => RedirectToAction("Details", "Bills", new { id = billid }));
            }
            else
            {
                var user = await _ur.GetByIdAsync(id);
                user.Bills.Add(b);
                await  _ct.SaveChangesAsync();
                return await Task.Run(() => RedirectToAction("Details", "Clients", new { id = id }));
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            var res = await _ct.Bills.Where(x => x.ID == id).Include(c => c.Users).FirstOrDefaultAsync();
            return View(res);
        }

        /*public async Task<IActionResult> Search(int id)
        {
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            var res = await _ct.Bills.Where(x => x.ID == id).Include(c => c.Users).FirstOrDefaultAsync();
            return View(res);
        }*/
        [HttpGet]
        [ActionName("assign")]
        public async Task<IActionResult> AssignClientToBill(int billid, int clientid)
        {
            var res_bill = await _br.GetByIdAsync(billid);
            var res_client = await _ur.GetByIdAsync(clientid);

            res_bill.Users.Add(res_client);
            await _ct.SaveChangesAsync();
            //return View(await _ct.Bills.Where(x => x.User.ID > 0).Include(c => c.User).ToListAsync());
            
            return RedirectToAction("Details","Bills", new { id=billid});
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

    }
}
