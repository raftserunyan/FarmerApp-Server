using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class InvestmentService : IInvestmentService
    {
        private IInvestmentRepository _investmentRepository;
        public InvestmentService(IInvestmentRepository investmentRepository)
        {
            _investmentRepository = investmentRepository;
        }

        public void SetUser(int userId)
        {
            _investmentRepository.SetUser(userId);
        }

        public List<Investment> GetAll() => _investmentRepository.GetAll();

        public int Add(Investment investment) => _investmentRepository.Add(investment);

        public void Remove(int id) => _investmentRepository.Remove(id);

        public Investment GetById(int id) => _investmentRepository.GetById(id);

        public Investment Update(Investment investment) => _investmentRepository.Update(investment);
    }
}
