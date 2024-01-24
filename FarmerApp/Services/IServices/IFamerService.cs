using FarmerApp.Models;
using FarmerApp.Models.ViewModels.ResponseModels;

namespace FarmerApp.Services.IServices
{
    public interface IFarmerService
    {
        void SetUser(int userId);
        Balance GetBalance();
        Balance GetBalanceByCustomer(int id);
        Balance GetInvestorBalance(int id);
        Balance GetInvestorBalance(Investor investor);
        Dictionary<string, object> GetProductsIncome();
        Dictionary<string, object> GetInvestorsBalance();
        IEnumerable<CustomerBalanceResponseModel> GetCustomersBalance();
    }
}