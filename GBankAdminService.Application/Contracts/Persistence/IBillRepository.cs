using GBankAdminService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Contracts.Persistence
{
    public interface IBillRepository : IAsyncRepository<Bill>
    {
        Task DeleteByIdAsync(int id);
        Task<List<Bill>> FindByAnyColumn(String phase);
        Task<List<Bill>> FindByBillNumber(String billnr);

    }
}
