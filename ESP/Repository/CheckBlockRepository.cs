using ESP.Context;
using ESP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESP.Repository
{
    public class CheckBlockRepository : IRepository<CheckBlock>,IDisposable
    {
        ApplicationContext applicationContext = null!;

        public CheckBlockRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public int Add(CheckBlock element)
        {
            applicationContext.CheckBlocks.Add(element);
            return applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            applicationContext.CheckBlocks.Remove(GetById(id));
            return applicationContext.SaveChanges();
        }

        public IQueryable<CheckBlock> GetAll()
        {
            return applicationContext.CheckBlocks;
        }

        public CheckBlock GetById(int id)
        {
            return applicationContext?.CheckBlocks?.Include(x => x.Block)
                                                   .Include(x => x.SubjectTypes)
                                                   .Include(x => x.CheckCodes)
                                                   .ThenInclude(x => x.ProhibitionCodes)
                                                   .FirstOrDefault(x => x.Id == id) ?? new CheckBlock();
        }


        public int Update(CheckBlock element)
        {
            applicationContext.CheckBlocks.Update(element);
            return applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext?.Dispose();
        }
    }
}
