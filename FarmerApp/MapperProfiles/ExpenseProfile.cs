using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseRequestModel, Expense>();
            CreateMap<Expense, ExpenseResponseModel>();
            CreateMap<Expense, Expense>(); 
        }
    }
}