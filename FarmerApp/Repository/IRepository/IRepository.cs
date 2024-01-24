namespace FarmerApp.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void SetUser(int userId);
        List<T> GetAll();
        int Add(T t);
        void Remove(int id);
        T Update(T t);
        T GetById(int id);
    }
}