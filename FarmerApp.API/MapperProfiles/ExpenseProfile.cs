using AutoMapper;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class ExpenseProfile : Profile
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseRequestModel, ExpenseModel>();
            CreateMap<ExpenseModel, ExpenseResponseModel>();
        }
    }
}