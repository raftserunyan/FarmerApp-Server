using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
    public class InvestmentRepository : IInvestmentRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private int _userId; //private User _user;

        public InvestmentRepository(//IUserRepository userRepository,
            FarmerDbContext dbContext,
            IMapper mapper
            )
        {
            //_userRepository = userRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void SetUser(int userId)
        {
            _userId = userId; //_user = _userRepository.GetById(userId);
        }

        public List<Investment> GetAll() => _dbContext.Investors.AsNoTracking().Include(x => x.Investments).ThenInclude(x => x.Investor).SelectMany(x => x.Investments).ToList();

        public int Add(Investment investment)
        {
            _dbContext.Investments.Add(investment);
            _dbContext.SaveChanges();

            return investment.Id;
        }

        public void Remove(int id)
        {
            _dbContext.Investments.Remove(_dbContext.Investments.SingleOrDefault(x => x.Id == id));
            _dbContext.SaveChanges();
        }

        public Investment Update(Investment investment)
        {
            var investmentToUpdate = _dbContext.Investments.SingleOrDefault(x => x.Id == investment.Id);

            _mapper.Map(investment, investmentToUpdate);

            _dbContext.SaveChanges();

            return investment;
        }

        public Investment GetById(int id) => _dbContext.Investments.Include(x => x.Investor).SingleOrDefault(x => x.Id == id);

    }
}
