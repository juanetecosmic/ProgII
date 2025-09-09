using Problema1_8.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Data.Interfaces
{
    public interface IRepository <T> where T : class
    {
        
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T GetById(int id);
        List<T> GetAll();
    }
}
