using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace ApiFactura.ODB
{
    public class ObjectDataBase : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        // Constructor que obtiene la cadena de conexión desde IConfiguration
        public ObjectDataBase(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        // Método para abrir la conexión
        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        // Método para cerrar la conexión
        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        // Propiedad para obtener la conexión actual
        public SqlConnection Connection => _connection;

        // Método para ejecutar un procedimiento almacenado que no devuelve resultados (como un INSERT o UPDATE)
        public bool ExecuteSP(string nombreProcedimiento, List<Parameters> parameters = null)
        {
            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (var parametro in parameters)
                        {
                            cmd.Parameters.AddWithValue(parametro.nombre, parametro.valor);
                        }
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error
                Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        // Método para ejecutar un procedimiento almacenado que devuelve resultados en un DataTable
        public DataTable ExecuteGSP(string nombreProcedimiento, List<SqlParameter> parameters)
        {
            DataTable resultado = new DataTable();

            try
            {
                OpenConnection();
                using (SqlCommand cmd = new SqlCommand(nombreProcedimiento, _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        // Añade los parámetros directamente sin necesidad de AddWithValue
                        foreach (var parametro in parameters)
                        {
                            cmd.Parameters.Add(parametro);
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(resultado);
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí podrías registrar el error
                Console.WriteLine($"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                return null;
            }
            finally
            {
                CloseConnection();
            }

            return resultado;
        }

        // Implementación de IDisposable para liberar la conexión
        public void Dispose()
        {
            if (_connection != null)
            {
                CloseConnection();
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
