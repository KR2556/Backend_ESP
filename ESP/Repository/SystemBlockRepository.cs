using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class SystemBlockRepository : IRepository<SystemBlock>, IDisposable
    {
        ApplicationContext applicationContext = null!;

        public SystemBlockRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }


        public int Add(SystemBlock element)
        {
            applicationContext.SystemBlocks.Add(element);
            return applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            applicationContext.SystemBlocks.Remove(GetById(id));
            return applicationContext.SaveChanges();
        }

        public IQueryable<SystemBlock> GetAll()
        {
            return applicationContext.SystemBlocks;
        }

        public SystemBlock GetById(int id)
        {
            return applicationContext.SystemBlocks.Find(id) ?? new SystemBlock();
        }

        public int Update(SystemBlock element)
        {
            applicationContext.SystemBlocks.Update(element);
            return applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext?.Dispose();
        }
    }
}
