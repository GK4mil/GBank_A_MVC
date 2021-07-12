using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using GBankAdminService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Infrastructure.Repositories.EF
{
    public class AdminRepository : BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository (GBankDbContext dbContext):base(dbContext)
        {

        }

        public async Task<List<Admin>> GetAdminByUsernameAndPasswd(string username, string password)
        {
            return await _dbContext.Admins.Where(x => x.Username == username && x.Password == password).ToListAsync();
        }
    }
}
