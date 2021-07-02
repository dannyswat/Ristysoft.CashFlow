using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Data;
using Ristysoft.CashFlow.Models;
using Ristysoft.CashFlow.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Services
{
	public class FundTransferService
	{
        readonly ApplicationDbContext _context;
        public FundTransferService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<FundTransfer> GetAsync(int id)
        {
            return _context.FundTransfers
                .Include(s => s.From)
                .Include(s => s.To)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<bool> FundTransferExists(int id)
        {
            return _context.FundTransfers.AnyAsync(e => e.Id == id);
        }

        public Task<List<FundTransfer>> ListAsync(ListItemsModel listModel)
        {
            IQueryable<FundTransfer> qry = _context.FundTransfers
                .Include(s => s.From)
                .Include(s => s.To);

            Filter(ref qry, listModel?.Filters);
            Sort(ref qry, listModel?.Sort);
            Page(ref qry, listModel);

            return qry.ToListAsync(); ;
        }

        public Task<int> CountAsync(IEnumerable<ListItemsFilter> filters)
        {
            IQueryable<FundTransfer> qry = _context.FundTransfers;

            Filter(ref qry, filters);

            return qry.CountAsync();
        }

        public async Task CreateAsync(FundTransfer FundTransfer)
        {
            _context.FundTransfers.Add(FundTransfer);

            if (FundTransfer.Amount > 0)
            {
                var fromFund = await _context.Funds.FindAsync(FundTransfer.FromFundId);
                var toFund = await _context.Funds.FindAsync(FundTransfer.ToFundId);

                fromFund.Balance -= FundTransfer.Amount;
                toFund.Balance += FundTransfer.Amount;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var FundTransfer = await _context.FundTransfers.FindAsync(id);

            if (FundTransfer != null)
            {
                if (FundTransfer.Amount > 0)
                {
                    var fromFund = await _context.Funds.FindAsync(FundTransfer.FromFundId);
                    var toFund = await _context.Funds.FindAsync(FundTransfer.ToFundId);

                    fromFund.Balance += FundTransfer.Amount;
                    toFund.Balance -= FundTransfer.Amount;
                }

                _context.FundTransfers.Remove(FundTransfer);
                await _context.SaveChangesAsync();
            }
        }

        void Page(ref IQueryable<FundTransfer> qry, ListItemsModel pager)
        {
            if (pager == null) return;

            qry = qry.Skip(pager.SkipRecordsCount)
                .Take(pager.PageSize);
        }

        void Filter(ref IQueryable<FundTransfer> qry, IEnumerable<ListItemsFilter> filters)
        {
            if (filters == null) return;
        }

        void Sort(ref IQueryable<FundTransfer> qry, string sort)
        {
            switch (sort)
            {
                case nameof(FundTransfer.Date) + "_desc":
                    qry = qry.OrderByDescending(r => r.Date);
                    break;
                case nameof(FundTransfer.Date):
                    qry = qry.OrderBy(r => r.Date);
                    break;
                default:
                    qry = qry.OrderByDescending(r => r.Date);
                    break;
            }
        }
    }
}
