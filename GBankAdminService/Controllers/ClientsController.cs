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
    public class ClientsController : Controller
    {
        private readonly GBankDbContext _ct;
        private readonly IUserRepository _ur;
        private readonly IBillRepository _br;
        public ClientsController(GBankDbContext ct, IUserRepository ur, IBillRepository br)
        {
            _ct = ct;
            _ur = ur;
            _br = br;
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
            await _ct.Users.AddAsync(u);
            await _ct.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _ur.DeleteUserByID(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var res = await Task.Run(() => _ct.Users.Where(x => x.ID ==id).ToList().First());
            res.ID = 2;
            return View(res);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User u)
        {
            await _ur.UpdateAsync(u);
            return View();
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

    }
}
