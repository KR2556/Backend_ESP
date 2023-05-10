using ESP.Context;
using ESP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESP.Repository
{
    public class ClientTypeRepository : IRepository<ClientType>, IDisposable
    {
        ApplicationContext applicationContext = null!;

        public ClientTypeRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public int Add(ClientType element)
        {
            applicationContext.ClientTypes.Add(element);
            return applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            applicationContext.ClientTypes.Remove(GetById(id));
            return applicationContext.SaveChanges();
        }

        public IQueryable<ClientType> GetAll()
        {
            return applicationContext.ClientTypes;
        }

        public ClientType GetById(int id)
        {
            return applicationContext.ClientTypes.Include(x => x.SubjectTypes).FirstOrDefault(x => x.Id == id) ?? new ClientType();
        }

        public int Update(ClientType element)
        {
            applicationContext.Update(element);
            return applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext?.Dispose();
        }
    }
}
