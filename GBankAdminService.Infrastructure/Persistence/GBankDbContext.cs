using System;
using GBankAdminService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace GBankAdminService.Infrastructure.Persistence
{
    public partial class GBankDbContext : DbContext
    {
        public GBankDbContext()
        {
        }

        public GBankDbContext(DbContextOptions<GBankDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<AdminPrivilege> AdminPrivileges { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }

        public static GBankDbContext Create()
        {
            return new GBankDbContext();
        }
    }
}
