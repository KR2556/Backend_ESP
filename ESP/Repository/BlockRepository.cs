using ESP.Context;
using ESP.Models;

namespace ESP.Repository
{
    public class BlockRepository : IRepository<Block>, IDisposable
    {

        ApplicationContext _applicationContext = null!;

        public BlockRepository(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
        }

        public int Add(Block element)
        {
            _applicationContext.Blocks.Add(element);
            return _applicationContext.SaveChanges();
        }

        public int Delete(int id)
        {
            _applicationContext.Blocks.Remove(GetById(id));
            return _applicationContext.SaveChanges();
        }

        public IQueryable<Block> GetAll()
        {
            return _applicationContext.Blocks;
        }

        public Block GetById(int id)
        {
            return _applicationContext.Blocks.Find(id) ?? new Block();
        }

        public int Update(Block element)
        {
            var blockToUpdate = GetById(element.Id);
            blockToUpdate.Name = element.Name;
            return _applicationContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationContext.Dispose();
        }
    }
}
