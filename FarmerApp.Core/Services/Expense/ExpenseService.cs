using System.Linq.Expressions;
using AutoMapper;
using FarmerApp.Core.Models.Expense;
using FarmerApp.Core.Query;
using FarmerApp.Core.Services.Common;
using FarmerApp.Core.Wrappers;
using FarmerApp.Data.Entities;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Specifications.Common;
using FarmerApp.Data.Specifications.Expense;
using FarmerApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Core.Services.Expense
{
    internal class ExpenseService : BaseService<ExpenseModel, ExpenseEntity>, IExpenseService
    {
        public ExpenseService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<PagedExpensesResult<ExpenseModel>> GetAllWithTotalAmount(ISpecification<ExpenseEntity> specification = null, BaseQueryModel query = null, bool includeDeleted = false, int depth = 1, IEnumerable<string> propertyTypesToExclude = null)
        {
            specification ??= new EmptySpecification<ExpenseEntity>();

            var expenses = _uow.Repository<ExpenseEntity>();
            var totalAmount = await expenses.Sum(x => x.Amount);
            var currentYearAmount = expenses
                .GetAllBySpecificationQueryable(new ExpensesOfCurrentYearSpecification(), includeDeleted)
                .Sum(x => x.Amount);
            
            var totalFilteredAmount = 0d;
            var currentYearFilteredAmount = 0d;

            int? total = null;
            if (query is not null)
            {
                FilterResults(specification, query);

                total = await _uow.Repository<ExpenseEntity>().Count(specification, includeDeleted);
                totalFilteredAmount = expenses
                    .GetAllBySpecificationQueryable(specification, includeDeleted)
                    .Sum(x => x.Amount);

                var specificationWithDateIgnored = GetSpecificationWithDateIgnored(specification);
                currentYearFilteredAmount = expenses
                    .GetAllBySpecificationQueryable(specificationWithDateIgnored, includeDeleted)
                    .Sum(x => x.Amount);

                IncludeDependenciesByDepth(specification, depth, propertyTypesToExclude);
                OrderResults(specification, query.Orderings);
                ApplyPaging(specification, query);
            }

            var entities = await expenses.GetAllBySpecification(specification, includeDeleted);

            return new PagedExpensesResult<ExpenseModel>
            {
                Results = _mapper.Map<List<ExpenseModel>>(entities),
                Total = total ?? entities.Count,
                PageNumber = query?.PageNumber ?? 1,
                PageSize = query?.PageSize ?? (total ?? entities.Count),
                TotalExpensesAmount = totalAmount,
                CurrentYearExpensesAmount = currentYearAmount,
                TotalFilteredAmount = totalFilteredAmount,
                CurrentYearFilteredAmount = currentYearFilteredAmount
            };
        }

        private ISpecification<ExpenseEntity> GetSpecificationWithDateIgnored(ISpecification<ExpenseEntity> specification)
        {
            var specificationWithDateIgnored = new EmptySpecification<ExpenseEntity>();
            if (specification.Criteria != null)
            {
                var criteriaWithoutDates = new RemoveDateFilterVisitor()
                    .VisitAndConvert(specification.Criteria, nameof(GetAllWithTotalAmount));

                Expression<Func<ExpenseEntity, bool>> dateFilter = x => EF.Functions.DateDiffYear(x.Date, DateTime.Now) == 0;
                specificationWithDateIgnored.Criteria = Expression.Lambda<Func<ExpenseEntity, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(criteriaWithoutDates, dateFilter.Parameters),
                        dateFilter.Body
                    ),
                    dateFilter.Parameters
                );
            }

            return specificationWithDateIgnored;
        }
    }
}