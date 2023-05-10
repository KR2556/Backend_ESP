namespace ESP.Repository
{
    public interface IRepository<T>
    {

        T GetById(int id);
        int Add(T element);
        int Update(T element);
        int Delete(int id);
        IQueryable<T> GetAll();
    }
}
