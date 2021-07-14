using GBankAdminService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Functions.Bills.Command
{
	public class AddBillCommand : IRequest<Bill>
	{
		public String balance { get; set; }
		
	}
}
