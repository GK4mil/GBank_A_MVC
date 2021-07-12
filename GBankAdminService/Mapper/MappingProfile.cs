using AutoMapper;
using GBankAdminService.Application.Functions.Login.Query;
using GBankAdminService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CredentialsModel, SearchUserToLoginQuery>();
        }
    }
}
