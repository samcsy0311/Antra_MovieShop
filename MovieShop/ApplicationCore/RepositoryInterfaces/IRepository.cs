using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
     public interface IRepository<T> where T : class
     {
          Task<T> GetById(int id);
          Task<List<T>> GetAll();
          Task<T> Add(T entity);
          Task<T> Update(T entity);
          Task<T> Delete(int id);
     }
}
