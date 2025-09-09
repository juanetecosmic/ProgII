using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problema1_8.Data.DataHelper
{
    public class DataHelper
    {
        private static DataHelper _instance; //declaramos un atributo estatico de la misma clase
        private readonly SqlConnection _connection; //atributo de conexion
        private DataHelper() //constructor privado para evitar instanciacion externa
        {
            _connection = new SqlConnection(Properties.Resources.Connection); //inicializamos la conexion
        }

        public static DataHelper GetInstance() //metodo estatico para obtener la instancia unica
        {
            if (_instance == null)
            {
                _instance = new DataHelper(); //si no existe la instancia, la creamos
            }
            return _instance; //devolvemos la instancia unica
        }

        public DataTable? ExecuteSP(string sp, List<Parameters>? par) //metodo para ejecutar un procedimiento almacenado con o sin parametros
        {
            DataTable? table = new DataTable();
            try
            {
                using SqlCommand command = new SqlCommand(sp, _connection); //usamos "using" para asegurar la disposicion del objeto
                command.CommandType = CommandType.StoredProcedure;
                if (par != null) //si hay parametros, los agregamos
                {
                    foreach (var p in par)
                    {
                        command.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value); //si el valor es null, usamos DBNull.Value
                    }
                }
                _connection.Open();
                using var dr = command.ExecuteReader();
                table.Load(dr);
                if (table.Rows.Count == 0)
                {
                    table = null; //si no hay filas, devolvemos null
                }
            }
            catch (Exception ex)
            {
                table = null;
                throw new Exception("Error executing stored procedure: " + ex.Message);
            }
            finally //aseguramos que la conexion se cierre
            {
                if (_connection.State == ConnectionState.Open)
                    _connection.Close();
            }
            return table;
        }
    }
}
