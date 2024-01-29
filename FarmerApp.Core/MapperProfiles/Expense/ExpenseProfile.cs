using AutoMapper;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Data.Entities;

namespace FarmerApp.Core.MapperProfiles.Expense
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseModel, ExpenseEntity>().ReverseMap();
            CreateMap<ExpenseEntity, ExpenseEntity>();
        }
    }
}

