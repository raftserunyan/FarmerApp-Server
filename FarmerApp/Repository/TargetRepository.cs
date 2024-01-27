using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
    public class TargetRepository : ITargetRepository
    {
        private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private int _userId; //private User _user;

        public TargetRepository(//IUserRepository userRepository,
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

        public List<Target> GetAll() => _dbContext.Targets.AsNoTracking().ToList();

        public int Add(Target target)
        {
            target.UserId = _userId; //_user.Id;
            _dbContext.Targets.Add(target);
            _dbContext.SaveChanges();

            return target.Id;
        }

        public void Remove(int Id)
        {
            _dbContext.Targets.Remove(_dbContext.Targets.SingleOrDefault(x => x.Id == Id));
            _dbContext.SaveChanges();
        }

        public Target GetById(int Id) => _dbContext.Targets.AsNoTracking().SingleOrDefault(x => x.Id == Id);

        public Target Update(Target target)
        {
            target.UserId = _userId; //_user.Id;
            var targetToUpdate = _dbContext.Targets.SingleOrDefault(x => x.Id == target.Id);

            _mapper.Map(target, targetToUpdate);

            _dbContext.SaveChanges();

            return target;
        }
    }
}
