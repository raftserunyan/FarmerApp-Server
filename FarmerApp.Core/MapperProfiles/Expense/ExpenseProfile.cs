using FarmerApp.Core.MapperProfiles.Common;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;

namespace FarmerApp.Core.MapperProfiles.Expense
{
    public class ExpenseProfile : BaseProfile<ExpenseEntity>
    {
        public ExpenseProfile()
        {
            CreateMap<ExpenseModel, ExpenseEntity>().ReverseMap();
            CreateMap<ExpenseEntity, ExpenseEntity>()
                .IncludeBase<BaseEntity, BaseEntity>()
                .ForMember(d => d.User, opts => opts.Ignore())
                .ForMember(d => d.Investor, opts => opts.Ignore())
                .ForMember(d => d.Target, opts => opts.Ignore());
        }
    }
}

