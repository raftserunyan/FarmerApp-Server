using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
	internal class SaleService : ISaleService
	{
		private ISaleRepository _saleRepository;

        public SaleService(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public void SetUser(int userId)
        {
            _saleRepository.SetUser(userId);
        }

        public List<Sale> GetAll() => _saleRepository.GetAll();

        public int Add(Sale sale)
        {
            _saleRepository.Add(sale);

            return sale.Id;
        }

        public void Remove(int id)
        {
            _saleRepository.Remove(id);
        }

        public Sale GetById(int id) => _saleRepository.GetById(id);

        public IEnumerable<Sale> GetSalesByProductId(int id) => _saleRepository.GetSalesByProductId(id);

        public IEnumerable<Sale> GetSalesByCustomerId(int id) => _saleRepository.GetSalesByCustomerId(id);

        public Sale Update(Sale sale) => _saleRepository.Update(sale);
    }
}

