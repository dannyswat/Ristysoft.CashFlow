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
    public class ExpenseService
    {
        readonly ApplicationDbContext _context;
        public ExpenseService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public Task<Expense> GetAsync(int id)
        {
            return _context.Expenses
                .Include(s => s.ExpenseType)
                .Include(s => s.PaidBy)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<bool> ExpenseExists(int id)
        {
            return _context.Expenses.AnyAsync(e => e.Id == id);
        }

        public Task<List<Expense>> ListAsync(ListItemsModel listModel)
        {
            IQueryable<Expense> qry = _context.Expenses
                .Include(s => s.ExpenseType)
                .Include(s => s.PaidBy);

            Filter(ref qry, listModel?.Filters);
            Sort(ref qry, listModel?.Sort);
            Page(ref qry, listModel);

            return qry.ToListAsync(); ;
        }

        public Task<int> CountAsync(IEnumerable<ListItemsFilter> filters)
        {
            IQueryable<Expense> qry = _context.Expenses;

            Filter(ref qry, filters);

            return qry.CountAsync();
        }

        public async Task CreateAsync(Expense Expense)
        {
            _context.Expenses.Add(Expense);

            if (Expense.Amount > 0)
            {
                var fund = await _context.Funds.FindAsync(Expense.PaidByFundId);
                fund.Balance -= Expense.Amount;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense Expense)
        {
            var origExpense = await _context.Expenses.AsNoTracking().FirstOrDefaultAsync(r => r.Id == Expense.Id);

            _context.Attach(Expense).State = EntityState.Modified;

            if (origExpense.PaidByFundId != Expense.PaidByFundId)
            {
                var origFund = await _context.Funds.FindAsync(origExpense.PaidByFundId);
                var fund = await _context.Funds.FindAsync(Expense.PaidByFundId);
                fund.Balance -= Expense.Amount;
                origFund.Balance += origExpense.Amount;
            }
            else
            if (origExpense.Amount != Expense.Amount)
            {
                var fund = await _context.Funds.FindAsync(Expense.PaidByFundId);
                fund.Balance -= Expense.Amount - origExpense.Amount;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Expense = await _context.Expenses.FindAsync(id);

            if (Expense != null)
            {
                if (Expense.Amount > 0)
                {
                    var fund = await _context.Funds.FindAsync(Expense.PaidByFundId);
                    fund.Balance += Expense.Amount;
                }

                _context.Expenses.Remove(Expense);
                await _context.SaveChangesAsync();
            }
        }

        void Page(ref IQueryable<Expense> qry, ListItemsModel pager)
        {
            if (pager == null) return;

            qry = qry.Skip(pager.SkipRecordsCount)
                .Take(pager.PageSize);
        }

        void Filter(ref IQueryable<Expense> qry, IEnumerable<ListItemsFilter> filters)
        {
            if (filters == null) return;
        }

        void Sort(ref IQueryable<Expense> qry, string sort)
        {
            switch (sort)
            {
                case nameof(Expense.Date) + "_desc":
                    qry = qry.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    break;
                case nameof(Expense.Date):
                    qry = qry.OrderBy(r => r.Date).ThenBy(r => r.Id);
                    break;
                case nameof(Expense.ExpenseType):
                    qry = qry.OrderBy(r => r.ExpenseType.Name).ThenByDescending(r => r.Id);
                    break;
                case nameof(Expense.PaidBy):
                    qry = qry.OrderBy(r => r.PaidBy.Name).ThenByDescending(r => r.Id);
                    break;
                default:
                    qry = qry.OrderByDescending(r => r.Date).ThenByDescending(r => r.Id);
                    break;
            }
        }
    }
}
