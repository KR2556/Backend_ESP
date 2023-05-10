using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class SystemTypeRepository : IRepository<SystemType>, IDisposable
    {
        
        ApplicationContext applicationContext = null!;

        public SystemTypeRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public int Add(SystemType element)
        {
            applicationContext.SystemTypes.Add(element);
            return applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            applicationContext.SystemTypes.Remove(GetById(id));
            return applicationContext.SaveChanges();
        }

        public IQueryable<SystemType> GetAll()
        {
            return applicationContext.SystemTypes;
        }

        public SystemType GetById(int id)
        {
            return applicationContext.SystemTypes.Find(id) ?? new SystemType();
        }

        public int Update(SystemType element)
        {
            applicationContext.SystemTypes.Update(element);
            return applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext.Dispose();
        }

    }
}
