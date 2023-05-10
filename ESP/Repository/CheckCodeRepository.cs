using ESP.Context;
using ESP.Models;
using Microsoft.EntityFrameworkCore;

namespace ESP.Repository
{
    public class CheckCodeRepository : IRepository<CheckCode>,IDisposable
    {

        ApplicationContext _applicationContext = null!;

        public CheckCodeRepository(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
        }
        public int Add(CheckCode checkCode)
        {
            _applicationContext.CheckCodes.Add(checkCode);
            return _applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            _applicationContext.CheckCodes.Remove(GetById(id));
            return _applicationContext.SaveChanges();
        }

        public IQueryable<CheckCode> GetAll()
        {
            return _applicationContext.CheckCodes.Include(x => x.ProhibitionCodes);
        }

        public CheckCode GetById(int id)
        {
            return _applicationContext.CheckCodes.Include(x => x.ProhibitionCodes).FirstOrDefault(x => x.Id == id) ?? new CheckCode();
        }

        public int Update(CheckCode element)
        {
            _applicationContext.CheckCodes.Update(element);
            return _applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            _applicationContext.Dispose();
        }
    }
}
