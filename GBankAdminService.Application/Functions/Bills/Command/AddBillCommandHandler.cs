using GBankAdminService.Application.Contracts.Persistence;
using GBankAdminService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GBankAdminService.Application.Functions.Bills.Command
{
	public class AddBillCommandHandler : IRequestHandler<AddBillCommand, Bill>
	{
		private readonly IBillRepository _br;

		public AddBillCommandHandler(IBillRepository br)
		{
			_br = br;
		}
		private async Task<string> generateBillNumberAsync()
		{
			Random r = new Random();
			String result = "";
			bool flag = false;
			while (!flag)
			{
				while (result.Length < 6)
				{
					result += r.Next() % 10;
				}
				if ((await _br.FindByBillNumber(result)).Count == 0)
					flag = true;
				else
					result = "";
			}
			return result;
		}

		public async Task<Bill> Handle(AddBillCommand request, CancellationToken cancellationToken)
		{
			var generated_billnumber = await generateBillNumberAsync();
			Decimal balance_parsed;

			if (Decimal.TryParse(request.balance, out balance_parsed))
			{

				return ((Bill)(await _br.AddAsync(new Bill()
				{
					balance = balance_parsed,
					billNumber = generated_billnumber,

				})));
			}
			else
			{
				return null;
			}

		}
	}
}

	

