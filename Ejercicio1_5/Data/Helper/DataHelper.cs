using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio1_5.Data.UoW;
using Microsoft.Data.SqlClient;

namespace Ejercicio1_5.Data.Helper
{
    public class DataHelper
    {
        private static DataHelper? _instance;
        private SqlConnection _connection;
        private SqlTransaction? _transaction;
        private DataHelper()
        {
            _connection = new SqlConnection(Properties.Resources.Connection);
        }
        public static DataHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DataHelper();
            }
            return _instance;
        }
        public SqlConnection GetConnection()
        {
            if (_connection == null)
                _connection = new SqlConnection(Properties.Resources.Connection);

            return _connection;
        }
        public DataTable? ExecuteSP(string sp, List<Parameters>? parameters)
        {
            DataTable? table = new DataTable();
            try
            {
                if (_connection.State != ConnectionState.Open)
                _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (Parameters p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }
                table.Load(cmd.ExecuteReader());
            }
            catch (SqlException ex)
            {
                table = null;
                throw new Exception("Error de base de datos", ex);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open && _transaction != null)
                    _connection.Close();
            }
            return table;
        }

        public int ExecuteSPNonQuery(string sp, List<Parameters>? parameters)
        {
            int result = 0;
            try
            {
                if(_connection.State != ConnectionState.Open)
                    _connection.Open();
                var cmd = new SqlCommand(sp, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (Parameters p in parameters)
                    {
                        cmd.Parameters.AddWithValue(p.Name, p.Value);
                    }
                }
                int rows = cmd.ExecuteNonQuery();
                result = rows;
            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }
        public int ExecuteSPNonQuery(string sp, List<Parameters>? parameters, SqlConnection connection, SqlTransaction transaction)
        {
            int result = 0;
            using (var cmd = new SqlCommand(sp, connection, transaction))
            {
                
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        if (p.IsOut)
                        {
                            var paramOut = new SqlParameter(p.Name, p.Value)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(paramOut);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue(p.Name, p.Value);
                        }
                    }
                }
                int rows = cmd.ExecuteNonQuery();
                if (parameters != null)
                {
                    foreach (SqlParameter param in cmd.Parameters)
                    {
                        var paramOut = parameters.FirstOrDefault(x => x.Name == param.ParameterName);
                        if (paramOut != null && param.Direction == ParameterDirection.Output)
                            paramOut.Value = param.Value;
                    }
                }
                result = rows;
            }
            return result;
        }
    }
}
