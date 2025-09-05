using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Ejercicio1_5.Data.Helper
{
    public class DataHelper
    {
        private static DataHelper? _instance;
        private SqlConnection _connection;
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
                _connection.Close();
            }
            return table;
        }
    }
}
