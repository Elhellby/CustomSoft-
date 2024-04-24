using Api.Crud.Dto;
using Api.Crud.Models;
using Api.Crud.Utils;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductsController> _logger;

         private readonly GestionarProductos _gestionarProductos;

        public ProductsController(IConfiguration configuration, ILogger<ProductsController> logger, GestionarProductos gestionarProductos)
        {
            _configuration = configuration;
            _logger = logger;
            _gestionarProductos=gestionarProductos;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]long Id)
        {
            var result=await _gestionarProductos.Gestionar(TypeCrud.Select,new ProductsModel(){
                Id=Id
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostProducts(ProductsModel product)
        {
            var result=await _gestionarProductos.Gestionar(TypeCrud.Insert,product);
            
            return Ok(result);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateProducts(ProductsModel product)
        {
            var result=await _gestionarProductos.Gestionar(TypeCrud.Update,product);
            
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProducts([FromQuery]long Id)
        {
            var result=await _gestionarProductos.Gestionar(TypeCrud.Delete,new ProductsModel(){
                Id=Id
            });

            return Ok(result);
        }
    }
}
