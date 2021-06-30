using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Ristysoft.CashFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ristysoft.CashFlow.Data
{
	public class SeedData
	{
		public async static Task Initialize(IServiceProvider serviceProvider)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

			if ((await userManager.FindByNameAsync("admin")) == null)
				await userManager.CreateAsync(new IdentityUser("admin") { Email = "ristysoft@gmail.com", EmailConfirmed = true }, "PleaseChange");

			var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

			if (!dbContext.Funds.Any())
			{
				dbContext.Funds.AddRange(new Models.Fund
				{
					Name = "Cash"
				}, new Models.Fund
				{
					Name = "Octopus"
				}, new Models.Fund
				{
					Name = "Bank"
				});
			}

			if (!dbContext.ExpenseTypes.Any())
			{
				dbContext.ExpenseTypes.AddRange(new Models.ExpenseType
				{
					Name = "Meals"
				},
				new Models.ExpenseType
				{
					Name = "Snacks"
				},
				new Models.ExpenseType
				{
					Name = "Drinks"
				},
				new Models.ExpenseType
				{
					Name = "Home sundries"
				},
				new Models.ExpenseType
				{
					Name = "Medication"
				},
				new Models.ExpenseType
				{
					Name = "Baby"
				},
				new Models.ExpenseType
				{
					Name = "Transport"
				});
			}

			if (!dbContext.RevenueTypes.Any())
			{
				dbContext.RevenueTypes.AddRange(new RevenueType
				{
					Name = "Home fund"
				},
				new RevenueType
				{
					Name = "Business income"
				},
				new RevenueType
				{
					Name = "Other income"
				}
				);
			}

			await dbContext.SaveChangesAsync();
		}
	}
}
