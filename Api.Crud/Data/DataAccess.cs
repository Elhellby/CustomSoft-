using System.Data;
using Newtonsoft.Json;
using Npgsql;

namespace Api.Crud.Data
{
    public class DataAccess
    {
        private readonly string? _connectionString;

        public DataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public void ExecuteNonQuery(string query, NpgsqlParameter[]? parameters)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            connection.Open();
            command.ExecuteNonQuery();
        }

        public object ExecuteScalar(string query, NpgsqlParameter[]? parameters)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = new NpgsqlCommand(query, connection);

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            connection.Open();
            return command.ExecuteScalar();
        }

        public DataTable ExecuteReader(string query, NpgsqlParameter[]? parameters)
        {
            var dataTable = new DataTable();

            try
            {
                var connection = new NpgsqlConnection(_connectionString);
                var command = new NpgsqlCommand(query, connection);

                if (parameters != null)
                    command.Parameters.AddRange(parameters);

                connection.Open();
                using var reader = command.ExecuteReader();
                dataTable.Load(reader);
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return dataTable;
        }

        public List<T> ConvertirDataTableAProductos<T>(DataTable dataTable)
        {
            var productos = new List<T>();

            var inJson = JsonConvert.SerializeObject(dataTable);
            var items = JsonConvert.DeserializeObject<List<T>>(inJson);

            return items;
        }
    }

}