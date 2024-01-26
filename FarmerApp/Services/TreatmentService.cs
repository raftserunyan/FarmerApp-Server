using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class TreatmentService : ITreatmentService
    {
        private ITreatmentRepository _treatmentRepository;
        private IProductRepository _productRepository;

        public TreatmentService(ITreatmentRepository treatmentRepository, IProductRepository productRepository)
        {
            _treatmentRepository = treatmentRepository;
            _productRepository = productRepository;
        }

        public void SetUser(int userId)
        {
            _treatmentRepository.SetUser(userId);
        }

        public List<Treatment> GetAll() => _treatmentRepository.GetAll();

        public int Add(Treatment treatment)
        {
            var products = _productRepository.GetAll();
            var productIds = treatment.TreatedProductsIds.Split(' ').Select(x => int.Parse(x));

            treatment.Products = new List<Product>();
            products.Where(x => productIds.Contains(x.Id))
                .Select(x =>
                {
                    treatment.Products.Add(x);
                    return x;
                })
                .ToList();

            return _treatmentRepository.Add(treatment);
        }
        public void Remove(int id) => _treatmentRepository.Remove(id);

        public Treatment Update(Treatment treatment) => _treatmentRepository.Update(treatment);

        public Treatment GetById(int id) => _treatmentRepository.GetById(id);
    }
}

