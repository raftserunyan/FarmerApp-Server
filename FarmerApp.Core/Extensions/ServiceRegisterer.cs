﻿using FarmerApp.Core.Services.Balance;
using FarmerApp.Core.Services.Customer;
using FarmerApp.Core.Services.Expense;
using FarmerApp.Core.Services.Identity;
using FarmerApp.Core.Services.Investment;
using FarmerApp.Core.Services.Product;
using FarmerApp.Core.Services.Sale;
using FarmerApp.Core.Services.Target;
using FarmerApp.Core.Services.Treatment;
using FarmerApp.Core.Services.User;
using FarmerApp.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace FarmerApp.Core.Extensions
{
    public static class ServiceRegisterer
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IInvestmentService, InvestmentService>();
            services.AddScoped<IInvestorService, InvestorService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<ITargetService, TargetService>();
            services.AddScoped<ITreatmentService, TreatmentService>();

            return services;
        }
    }
}