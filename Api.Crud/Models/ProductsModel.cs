using System.ComponentModel.DataAnnotations;

namespace Api.Crud.Models
{
    public class ProductsModel
    {
        
        public long Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Descripcion { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public string? Categoria { get; set; }
        [Required]
        public int cantidad_en_stock { get; set; }
        [Required]
        public string? Proveedor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}