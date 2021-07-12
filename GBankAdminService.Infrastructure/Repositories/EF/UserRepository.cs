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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly GBankDbContext _ct;
        public UserRepository(GBankDbContext dbContext) : base(dbContext)
        {
            _ct = dbContext;
        }

        public async Task<int> DeleteUserByID(int id)
        {
            _ct.Users.Remove(_ct.Users.Where(x => x.ID == id).FirstOrDefault());
            return await _ct.SaveChangesAsync();
        }

        public async Task<List<User>> FindByAnyColumn(string phase)
        {
           
            return   await Task.Run(()=> _ct.Users.Where(x => x.firstname.Contains(phase)
             || x.lastname.Contains(phase) || x.Username.Contains(phase)).ToList());
        }
    }
}
