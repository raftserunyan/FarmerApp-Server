﻿using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Expense
{
    public class ExpenseProfile : BaseProfile<ExpenseEntity>
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseModel, ExpenseEntity>().ReverseMap();
            CreateMap<ExpenseEntity, ExpenseEntity>();
        }
    }
}

