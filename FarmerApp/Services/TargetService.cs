using FarmerApp.Exceptions;
using FarmerApp.Models;
using FarmerApp.Repository.IRepository;
using FarmerApp.Services.IServices;

namespace FarmerApp.Services
{
    public class TargetService : ITargetService
    {
        private ITargetRepository _targetRepository;
        public TargetService(ITargetRepository targetRepository)
        {
            _targetRepository = targetRepository;
        }

        public void SetUser(int userId)
        {
            _targetRepository.SetUser(userId);
        }

        public List<Target> GetAll() => _targetRepository.GetAll();

        public int Add(Target target) => _targetRepository.Add(target);

        public void Remove(int id) => _targetRepository.Remove(id);

        public Target GetById(int id) => _targetRepository.GetById(id) ?? throw new NotFoundException();

        public Target Update(Target target) => _targetRepository.Update(target);
    }
}
