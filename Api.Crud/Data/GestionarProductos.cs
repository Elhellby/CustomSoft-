using System.Data;
using System.Globalization;
using Api.Crud.Data;
using Api.Crud.Models;
using Api.Crud.Utils;
using Newtonsoft.Json;
using Npgsql;

namespace Api.Crud.Data
{
    public class GestionarProductos : DataAccess
    {
        private readonly ILogger<GestionarProductos> _logger;
        public GestionarProductos(IConfiguration configuration, ILogger<GestionarProductos> logger) : base(configuration)
        {
            _logger = logger;
        }

        public async Task<List<ProductsModel>> Gestionar(TypeCrud typeCrud, ProductsModel model)
        {
            _logger.LogInformation("PROCESANDO QUERY");
            List<ProductsModel> products = new List<ProductsModel>();
            DataTable table = new DataTable();
            switch (typeCrud)
            {
                case TypeCrud.Insert:
                    table = InsertarProducto(model);
                    break;
                case TypeCrud.Select:
                    table = LeerProducto(model);
                    break;
                case TypeCrud.Update:
                    table = ActualizarProducto(model);
                    break;
                case TypeCrud.Delete:
                    BorrarProducto(model);
                    break;
                default:
                    throw new ArgumentException("Estado no válido para la gestión de productos.");
            }
            products = ConvertirDataTableAProductos<ProductsModel>(table);
            return products;
        }

        private DataTable InsertarProducto(ProductsModel model)
        {
            _logger.LogInformation("INSERT . . .");

            string query = "SELECT * FROM gestionar_productos(@state, NULL, @nombre, @descripcion, @precio, @categoria, @cantidad, @proveedor)";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@state", "crear"),
                new NpgsqlParameter("@nombre", model.Nombre),
                new NpgsqlParameter("@descripcion", model.Descripcion ?? (object)DBNull.Value),
                new NpgsqlParameter("@precio", model.Precio),
                new NpgsqlParameter("@categoria", model.Categoria ?? (object)DBNull.Value),
                new NpgsqlParameter("@cantidad", model.CantidadEnStock),
                new NpgsqlParameter("@proveedor", model.Proveedor ?? (object)DBNull.Value)
            };

            var result = ExecuteReader(query, parameters);
            return result;
        }

        private DataTable LeerProducto(ProductsModel model)
        {
            _logger.LogInformation("SELECT . . .");

            string query = "SELECT * FROM gestionar_productos(@state, @id)";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@state", "leer"),
                new NpgsqlParameter("@id", model.Id)
            };

            var result = ExecuteReader(query, parameters);
            return result;
        }

        private DataTable ActualizarProducto(ProductsModel model)
        {
            _logger.LogInformation("UPDATE . . .");

            string query = "SELECT * FROM gestionar_productos(@state, @id, @nombre, @descripcion, @precio, @categoria, @cantidad, @proveedor)";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@state", "actualizar"),
                new NpgsqlParameter("@id", model.Id),
                new NpgsqlParameter("@nombre", model.Nombre ?? (object)DBNull.Value),
                new NpgsqlParameter("@descripcion", model.Descripcion ?? (object)DBNull.Value),
                new NpgsqlParameter("@precio", model.Precio),
                new NpgsqlParameter("@categoria", model.Categoria ?? (object)DBNull.Value),
                new NpgsqlParameter("@cantidad", model.CantidadEnStock),
                new NpgsqlParameter("@proveedor", model.Proveedor ?? (object)DBNull.Value)
            };

            var result = ExecuteReader(query, parameters);
            return result;
        }

        private DataTable BorrarProducto(ProductsModel model)
        {
            _logger.LogInformation("DELETE . . .");

            string query = "SELECT * FROM gestionar_productos(@state, @id)";
            var parameters = new NpgsqlParameter[]
            {
                new NpgsqlParameter("@state", "borrar"),
                new NpgsqlParameter("@id", model.Id)
            };

            var result = ExecuteReader(query, parameters);
            return result;
        }


    }
}
