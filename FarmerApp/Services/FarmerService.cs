using System.Runtime.CompilerServices;
using FarmerApp.Models;
using FarmerApp.Models.ViewModels.ResponseModels;
using FarmerApp.Repository;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class FarmerService : IFarmerService
    {
        private ISaleRepository _saleRepository;
        private IInvestmentRepository _investmentRepository;
        private IInvestorRepository _investorRepository;
        private IExpenseRepository _expenseRepository;
        private IProductRepository _productRepository;

        public FarmerService(
            IProductRepository productRepository,
            IExpenseRepository expenseRepository,
            IInvestmentRepository investmentRepository,
            IInvestorRepository investorRepository,
            ISaleRepository saleRepository)
        {
            _productRepository = productRepository;
            _expenseRepository = expenseRepository;
            _investmentRepository = investmentRepository;
            _investorRepository = investorRepository;
            _saleRepository = saleRepository;
        }

        public void SetUser(int userId)
        {
            _productRepository.SetUser(userId);
            _expenseRepository.SetUser(userId);
            _investmentRepository.SetUser(userId);
            _investorRepository.SetUser(userId);
            _saleRepository.SetUser(userId);
        }

        public Balance GetBalance()
        {
            var sales = _saleRepository.GetAll();
            var investments = _investmentRepository.GetAll();
            var expenses = _expenseRepository.GetAll();

            var balance = new Balance();

            var payed = sales.Sum(x => x.Payed);

            balance.Leftover += payed;
            balance.Leftover += (int)investments.Sum(x => x.Amount);
            balance.Leftover -= expenses.Sum(x => x.ExpenseAmount);

            balance.Debt = (int)sales.Sum(x => x.PriceKG * x.Weight) - payed;

            return balance;
        }

        public Balance GetInvestorBalance(int id)
        {
            var investor = _investorRepository.GetById(id);

            var balance = new Balance
            {
                Leftover = (int)investor.Investments.Sum(x => x.Amount)
            };

            return balance;
        }

        public Balance GetInvestorBalance(Investor investor)
        {
            var expenses = _expenseRepository.GetAll();

            var balance = new Balance
            {
                Leftover = (int)investor.Investments.Sum(x => x.Amount) - expenses.Sum(x => x.ExpenseAmount)
            };

            return balance;
        }

        public ProductBalanceResponseModel GetBalanceByProductId(int id)
        {
            var sales = _saleRepository.GetSalesByProductId(id);

            var balance = new ProductBalanceResponseModel
            {
                Weight = sales.Sum(x => x.Weight),
                Leftover = sales.Sum(x => x.Payed),
                Debt = (int)sales.Sum(x => x.PriceKG * x.Weight) - sales.Sum(x => x.Payed)
            };

            return balance;
        }

        public Balance GetBalanceByCustomer(int id)
        {
            var sales = _saleRepository.GetSalesByCustomerId(id);

            var balance = new Balance
            {
                Leftover = sales.Sum(x => x.Payed),
                Debt = (int)sales.Sum(x => x.PriceKG * x.Weight) - sales.Sum(x => x.Payed)
            };

            return balance;
        }        

        public Dictionary<string, object> GetProductsIncome()
        {
            Dictionary<string, object> incomes = new Dictionary<string, object>();

            var products = _productRepository.GetAll();

            foreach (var product in products)
            {
                var balance = GetBalanceByProductId(product.Id);

                incomes.Add(product.Name, balance);
            }

            return incomes;
        }

        public Dictionary<string, object> GetInvestorsBalance()
        {
            Dictionary<string, object> balances = new Dictionary<string, object>();

            var investors = _investorRepository.GetAll();

            foreach (var investor in investors)
            {
                var balance = GetInvestorBalance(investor);

                balances.Add(investor.Name, balance);
            }

            return balances;
        }

        public IEnumerable<CustomerBalanceResponseModel> GetCustomersBalance()
        {
            List<CustomerBalanceResponseModel> balances = new List<CustomerBalanceResponseModel>();

            var sales = _saleRepository.GetAll();

            foreach (var sale in sales)
            {
                var balance = GetBalanceByCustomer(sale.CustomerId);

                balances.Add(new CustomerBalanceResponseModel
                {
                    CustomerId = sale.CustomerId,
                    Leftover = balance.Leftover,
                    Debt = balance.Debt
                });
            }

            return balances.DistinctBy(x => x.CustomerId);
        }
    }
}