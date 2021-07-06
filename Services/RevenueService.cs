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
    public class RevenueService
    {
        readonly ApplicationDbContext _context;
        public RevenueService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<Revenue> GetAsync(int id)
        {
            return _context.Revenues
                .Include(s => s.RevenueType)
                .Include(s => s.ReceivedBy)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<bool> RevenueExists(int id)
        {
            return _context.Revenues.AnyAsync(e => e.Id == id);
        }

        public Task<List<Revenue>> ListAsync(ListItemsModel listModel)
        {
            IQueryable<Revenue> qry = _context.Revenues
                .Include(s => s.RevenueType)
                .Include(s => s.ReceivedBy);

            Filter(ref qry, listModel?.Filters);
            Sort(ref qry, listModel?.Sort);
            Page(ref qry, listModel);

            return qry.ToListAsync(); ;
        }

        public Task<int> CountAsync(IEnumerable<ListItemsFilter> filters)
        {
            IQueryable<Revenue> qry = _context.Revenues;

            Filter(ref qry, filters);

            return qry.CountAsync();
        }

        public async Task CreateAsync(Revenue Revenue)
        {
            _context.Revenues.Add(Revenue);

            if (Revenue.Amount > 0)
            {
                var fund = await _context.Funds.FindAsync(Revenue.ReceivedByFundId);
                fund.Balance += Revenue.Amount;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Revenue Revenue)
        {
            var origRevenue = await _context.Revenues.AsNoTracking().FirstOrDefaultAsync(r => r.Id == Revenue.Id);

            _context.Attach(Revenue).State = EntityState.Modified;

            if (origRevenue.ReceivedByFundId != Revenue.ReceivedByFundId)
            {
                var origFund = await _context.Funds.FindAsync(origRevenue.ReceivedByFundId);
                var fund = await _context.Funds.FindAsync(Revenue.ReceivedByFundId);
                fund.Balance += Revenue.Amount;
                origFund.Balance -= origRevenue.Amount;
            }
            else
            if (origRevenue.Amount != Revenue.Amount)
            {
                var fund = await _context.Funds.FindAsync(Revenue.ReceivedByFundId);
                fund.Balance += Revenue.Amount - origRevenue.Amount;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Revenue = await _context.Revenues.FindAsync(id);

            if (Revenue != null)
            {
                if (Revenue.Amount > 0)
                {
                    var fund = await _context.Funds.FindAsync(Revenue.ReceivedByFundId);
                    fund.Balance -= Revenue.Amount;
                }

                _context.Revenues.Remove(Revenue);
                await _context.SaveChangesAsync();
            }
        }

        void Page(ref IQueryable<Revenue> qry, ListItemsModel pager)
        {
            if (pager == null) return;

            qry = qry.Skip(pager.SkipRecordsCount)
                .Take(pager.PageSize);
        }

        void Filter(ref IQueryable<Revenue> qry, IEnumerable<ListItemsFilter> filters)
        {
            if (filters == null) return;
        }

        void Sort(ref IQueryable<Revenue> qry, string sort)
        {
            switch (sort)
            {
                case nameof(Revenue.Date) + "_desc":
                    qry = qry.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    break;
                case nameof(Revenue.Date):
                    qry = qry.OrderBy(r => r.Date).ThenBy(r => r.Id);
                    break;
                case nameof(Revenue.RevenueType):
                    qry = qry.OrderBy(r => r.RevenueType.Name).ThenByDescending(r => r.Id);
                    break;
                case nameof(Revenue.ReceivedBy):
                    qry = qry.OrderBy(r => r.ReceivedBy.Name).ThenByDescending(r => r.Id);
                    break;
                default:
                    qry = qry.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    break;
            }
        }
    }
}
