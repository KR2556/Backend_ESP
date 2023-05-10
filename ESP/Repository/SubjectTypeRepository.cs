using ESP.Context;
using ESP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESP.Repository
{
    public class SubjectTypeRepository : IRepository<SubjectType>, IDisposable
    {
        ApplicationContext _applicationContext = null!;

        public SubjectTypeRepository(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
        }
        public int Add(SubjectType element)
        {
            _applicationContext.SubjectTypes.Add(element);
            return _applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            _applicationContext.SubjectTypes.Remove(GetById(id));
            return _applicationContext.SaveChanges();
        }
        public IQueryable<SubjectType> GetAll()
        {
            return _applicationContext.SubjectTypes;
        }

        public SubjectType GetById(int id)
        {
            return _applicationContext.SubjectTypes.Include(x => x.CheckBlocks)
                                                   .ThenInclude(x => x.CheckCodes)
                                                   .FirstOrDefault(x => x.Id == id) ?? new SubjectType();
        }

        public int Update(SubjectType element)
        {
            _applicationContext.SubjectTypes.Update(element);
            return _applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            _applicationContext.Dispose();
        }

    }
}
