using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
	public class InvestorService : IInvestorService
	{
        private IInvestorRepository _investorRepository;
        public InvestorService(IInvestorRepository investorRepository)
        {
            _investorRepository = investorRepository;
        }

        public void SetUser(int userId)
        {
            _investorRepository.SetUser(userId);
        }

        public List<Investor> GetAll() => _investorRepository.GetAll();

        public int Add(Investor investor) => _investorRepository.Add(investor);

        public void Remove(int id) => _investorRepository.Remove(id);

        public Investor GetById(int id) => _investorRepository.GetById(id);

        public Investor Update(Investor investor) => _investorRepository.Update(investor);
    }
}

