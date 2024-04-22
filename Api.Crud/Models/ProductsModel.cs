namespace Api.Crud.Models
{
    public class ProductsModel
    {
        public long Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Categoria { get; set; }
        public int CantidadEnStock { get; set; }
        public string? Proveedor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}