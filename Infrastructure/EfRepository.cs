using Models;
using Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
  public class EfRepository<T> : IRepository<T> where T : BaseEntity
  {
    protected readonly SondageDbcontext _dbContext;

    public EfRepository(SondageDbcontext dbContext)
    {
      _dbContext = dbContext;
    }

    public T Add(T entity)
    {
      throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
      throw new NotImplementedException();
    }

    public T GetById(int id)
    {
      return _dbContext.Set<T>().Find(id);
    }

    public IEnumerable<T> ListAll()
    {
      return _dbContext.Set<T>().AsEnumerable();
    }

    public void Update(T entity)
    {
      throw new NotImplementedException();
    }
  }
}
