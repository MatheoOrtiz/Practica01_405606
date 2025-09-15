using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data.Helpers
{
    public class DataHelper : IDisposable
    {
        private static DataHelper _instancia;
        private SqlConnection _connection;

        private DataHelper()
        {
            _connection = new SqlConnection(@"Data Source=DESKTOP-AHJR1GS\SQLEXPRESS;Integrated Security=True;Encrypt=False");
        }

        public static DataHelper GetInstance()
        {
            if (_instancia == null)
                _instancia = new DataHelper();
            return _instancia;
        }

        private void EnsureOpen()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public DataTable ExecuteSPQuery(string sp, List<ParameterSQL> parametros = null)
        {
            DataTable t = new DataTable();
            try
            {
                EnsureOpen();
                using (var cmd = new SqlCommand(sp, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        foreach (var p in parametros)
                            cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

                    using (var dr = cmd.ExecuteReader())
                        t.Load(dr);
                }
            }
            finally
            {
                _connection.Close();
            }
            return t;
        }

        public int ExecuteSPDML(string sp, List<ParameterSQL> parametros = null)
        {
            int rows = 0;
            try
            {
                EnsureOpen();
                using (var cmd = new SqlCommand(sp, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        foreach (var p in parametros)
                            cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

                    rows = cmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _connection.Close();
            }
            return rows;
        }

        public object ExecuteScalar(string sp, List<ParameterSQL> parametros = null)
        {
            object result = null;
            try
            {
                EnsureOpen();
                using (var cmd = new SqlCommand(sp, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (parametros != null)
                        foreach (var p in parametros)
                            cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);

                    result = cmd.ExecuteScalar();
                }
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public SqlConnection GetConnection() => _connection;

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State != ConnectionState.Closed)
                    _connection.Close();
                _connection.Dispose();
            }
        }
    }
}
