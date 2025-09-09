using Ejercicio1_5.Data.Helper;
using Ejercicio1_5.Data.Implementation;
using Ejercicio1_5.Data.Interface;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio1_5.Data.UoW
{
    public class UnitOfWork
    {
        private static UnitOfWork _instance;
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;
        private IFacturaRepository _facturaRepository;
        private IDetalleRepository _detalleRepository;
        public UnitOfWork()
        {
            _connection = DataHelper.GetInstance().GetConnection();
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
            _transaction = _connection.BeginTransaction();
        }
        public IFacturaRepository FacturaRepository
        {
            get
            {             
                if (_facturaRepository == null)
                    _facturaRepository = new FacturaRepository(_connection,_transaction);
                return _facturaRepository;
            }
        }
        public IDetalleRepository DetalleRepository
        {
            get
            {
                if (_detalleRepository == null)
                    _detalleRepository = new DetalleRepository();
                return _detalleRepository;
            }
        }
        public void Rollback()
        {
            _transaction.Rollback();
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

        public void SaveChanges()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception("Error al guardar cambios en la BD", ex);
            }
        }
    }
}
