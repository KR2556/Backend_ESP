using ESP.Context;
using Microsoft.EntityFrameworkCore;

namespace ESP.Repository
{
    public class RouteRepository : IRepository<Models.Route>, IDisposable
    {
        ApplicationContext applicationContext = null!;

        public RouteRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public int Add(Models.Route element)
        {
            applicationContext.Routes.Add(element);
            return applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            applicationContext.Routes.Remove(GetById(id));
            return applicationContext.SaveChanges();
        }

        public IQueryable<Models.Route> GetAll()
        {
            return applicationContext.Routes.Include(x => x.ProhibitionCodes).Include(x => x.CheckCodes);
        }

        public Models.Route GetById(int id)
        {
            return applicationContext.Routes
                                     .Include(x => x.CheckCodes)
                                     .Include(x => x.ProhibitionCodes)
                                     .FirstOrDefault(x => x.Id == id) ?? new Models.Route();
        }

        public int Update(Models.Route element)
        {
            applicationContext.Routes.Update(element);
            return applicationContext.SaveChanges();
        }
        public void Dispose()
        {
            applicationContext.Dispose();
        }
    }
}
