namespace FarmerApp.Services.IServices
{
    public interface IService<T> where T : class
    {
        void SetUser(int userId);
        List<T> GetAll();
        int Add(T t);
        void Remove(int id);
        T Update(T t);
    }
}