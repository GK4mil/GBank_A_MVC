using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Infrastructure.Repositories.EF
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        private readonly GBankDbContext _ct;

        public BillRepository(GBankDbContext dbContext) : base(dbContext)
        {
            _ct = dbContext;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var result = await _ct.Set<Bill>().FindAsync(id);
            await Task.Run(()=> {
                _ct.Bills.Remove(result);
                _ct.SaveChanges();
                });
        }

        public async Task<List<Bill>> FindByAnyColumn(string phase)
        {

            return await Task.Run(() => _ct.Bills.Where(x => x.balance.ToString().Contains(phase)
            || x.billNumber.Contains(phase)).ToList());
        }
    }
}
