using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Implementation;
using Ejercicio1_5.Data.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private IFacturaRepository _facturaRepository;
        public UnitOfWork(string connectionString)
        {
            _connection = new SqlConnection(Properties.Resources.Connection);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
       // public IFacturaRepository FacturaRepository
      //  {
      //      get
     //       {
     //           if (_facturaRepository == null)
     //           {
     //               _facturaRepository = new FacturaRepository(_connection, _transaction);
     //           }
     //           return _facturaRepository;
     //       }
     //   }
        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception("Error al guardar cambios en la Base de Datos", ex);
            }
            finally
            {
                _connection.Close();
            }
        }
        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
