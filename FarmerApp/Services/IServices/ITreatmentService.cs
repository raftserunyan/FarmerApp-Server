using FarmerApp.Models;

namespace FarmerApp.Services.IServices
{
    public interface ITreatmentService : IService<Treatment>
    {
        Treatment GetById(int id);
    }
}