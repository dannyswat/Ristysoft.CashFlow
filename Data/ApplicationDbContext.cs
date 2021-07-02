using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ristysoft.CashFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ristysoft.CashFlow.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<Expense> Expenses { get; set; }

		public DbSet<ExpenseType> ExpenseTypes { get; set; }

		public DbSet<Revenue> Revenues { get; set; }

		public DbSet<RevenueType> RevenueTypes { get; set; }

		public DbSet<FundTransfer> FundTransfers { get; set; }

		public DbSet<Fund> Funds { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Expense>()
				.HasOne(r => r.ExpenseType)
				.WithMany()
				.HasForeignKey(r => r.ExpenseTypeId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Expense>()
				.HasOne(r => r.PaidBy)
				.WithMany()
				.HasForeignKey(r => r.PaidByFundId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Revenue>()
				.HasOne(r => r.RevenueType)
				.WithMany()
				.HasForeignKey(r => r.RevenueTypeId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<Revenue>()
				.HasOne(r => r.ReceivedBy)
				.WithMany()
				.HasForeignKey(r => r.ReceivedByFundId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<FundTransfer>()
				.HasOne(r => r.From)
				.WithMany()
				.HasForeignKey(r => r.FromFundId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<FundTransfer>()
				.HasOne(r => r.To)
				.WithMany()
				.HasForeignKey(r => r.ToFundId)
				.OnDelete(DeleteBehavior.Restrict);
		}

	}
}
