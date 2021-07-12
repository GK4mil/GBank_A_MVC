using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Functions.Login.Query
{
    public class SearchUserToLoginQueryHandler : IRequestHandler<SearchUserToLoginQuery, List<Admin>>
    {
        private readonly IAdminRepository _ar;
        public SearchUserToLoginQueryHandler(IAdminRepository ar)
        {
            this._ar = ar;
        }
        public Task<List<Admin>> Handle(SearchUserToLoginQuery request, CancellationToken cancellationToken)
        {
            return _ar.GetAdminByUsernameAndPasswd(request.username, request.password);
           
        }
    }
}
