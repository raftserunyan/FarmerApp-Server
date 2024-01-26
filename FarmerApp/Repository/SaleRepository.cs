using AutoMapper;
using FarmerApp.DataAccess.DB;
using FarmerApp.MapperProfiles;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Repository
{
	internal class SaleRepository : ISaleRepository
	{
		private FarmerDbContext _dbContext;
		private IMapper _mapper;
        private int _userId;

        public SaleRepository(FarmerDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void SetUser(int userId)
        {
            _userId = userId;
        }

        public List<Sale> GetAll()
        {
            return _dbContext.Sales
            .AsNoTracking()
            .Include(x => x.CurrentProduct)
            .Include(x => x.CurrentCustomer)
            .Where(x => x.UserId == _userId)
            .ToList();
        }

        public int Add(Sale sale)
        {
            sale.UserId = _userId;
            _dbContext.Sales.Add(sale);
			_dbContext.SaveChanges();

            return sale.Id;
        }

        public void Remove(int id)
        {
            _dbContext.Sales.Remove(_dbContext.Sales.SingleOrDefault(x => x.Id == id));
			_dbContext.SaveChanges();
        }

        public Sale GetById(int id) => _dbContext.Sales
            .Include(x => x.CurrentCustomer)
            .Include(x => x.CurrentProduct)
            .SingleOrDefault(x => x.Id == id);

        public IEnumerable<Sale> GetSalesByProductId(int id) => _dbContext.Sales
            .Include(x => x.CurrentCustomer)
            .Include(x => x.CurrentProduct)
            .Where(x => x.ProductId == id)
            .ToList();

        public IEnumerable<Sale> GetSalesByCustomerId(int id) => _dbContext.Sales
            .Include(x => x.CurrentCustomer)
            .Include(x => x.CurrentProduct)
            .Where(x => x.CustomerId == id)
            .ToList();

        public Sale Update(Sale sale)
        {
            sale.UserId = _userId;
            var saleToUpdate = _dbContext.Sales.SingleOrDefault(x => x.Id == sale.Id);

            _mapper.Map(sale, saleToUpdate);
            var check = saleToUpdate;
            _dbContext.SaveChanges();

            return sale;
        }
    }
}

