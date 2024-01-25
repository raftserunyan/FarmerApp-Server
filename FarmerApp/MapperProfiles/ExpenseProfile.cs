using AutoMapper;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.RequestModels;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.MapperProfiles
{
    public class ExpenseProfile : Profile
    {
        // private readonly ISaleService _saleService;
        public ExpenseProfile()
        {
            CreateMap<ExpenseRequestModel, Expense>()
                .ForMember(expense => expense.Date,
                    opts => opts.MapFrom(expenseRequest => DateTime.Now));

            CreateMap<Expense, ExpenseResponseModel>();
            CreateMap<Expense, Expense>();
        }
    }
}