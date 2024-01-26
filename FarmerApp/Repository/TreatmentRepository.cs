using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
	public class TreatmentRepository : ITreatmentRepository
	{
		private FarmerDbContext _dbContext;
        private IMapper _mapper;
        private IUserRepository _userRepository;
        private int _userId; //private User _user;

        public TreatmentRepository(//IUserRepository userRepository,
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

        public List<Treatment> GetAll() => _dbContext.Treatments.AsNoTracking().Include(x => x.Products).ToList();

		public int Add(Treatment treatment)
		{
            treatment.UserId = _userId; //_user.Id;

            _dbContext.Treatments.Add(treatment);
			_dbContext.SaveChanges();

            return treatment.Id;
        }

		public void Remove(int id)
        {
            _dbContext.Treatments.Remove(_dbContext.Treatments.AsNoTracking().SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Treatment GetById(int id) => _dbContext.Treatments.AsNoTracking().Include(x => x.Products).SingleOrDefault(x => x.Id == id);

        public Treatment Update(Treatment treatment)
        {
            treatment.UserId = _userId; //_user.Id;
            var treatmentToUpdate = _dbContext.Treatments.SingleOrDefault(x => x.Id == treatment.Id);

            _mapper.Map(treatment, treatmentToUpdate);

            _dbContext.SaveChanges();

            return treatment;
        }
    }
}

