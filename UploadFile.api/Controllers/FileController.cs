using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using UploadFile.api.Models;

namespace UploadFile.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<FileController> _logger;

        public FileController(IConfiguration configuration, ILogger<FileController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [RequestFormLimits(ValueCountLimit = int.MaxValue, MultipartBodyLengthLimit = long.MaxValue)]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            _logger.LogInformation("Inicia carga de archivos");
            if (files == null || files.Count == 0)
            {
                _logger.LogError("No se han proporcionado archivos para cargar.");
                return BadRequest("No se han proporcionado archivos para cargar.");
            }

            var uploadResults = new List<FileResultModel>();

            var connectionString = _configuration.GetValue<string>("AzureStorage:ConnectionString");
            var blobServiceClient = new BlobServiceClient(connectionString);

            var containerName = "customsoft";
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            foreach (var file in files)
            {
                var uploadFile = new FileResultModel
                {
                    FileName = file.FileName,
                    Extension = Path.GetExtension(file.FileName),
                    Size = file.Length,
                    Url = string.Empty,
                    BlobId = Guid.NewGuid().ToString(),
                    Exito = true,
                    Error=string.Empty
                };
                try
                {
                    var blobName = $"{Guid.NewGuid()}_{file.FileName}{Path.GetExtension(file.FileName)}";
                    var blobClient = blobContainerClient.GetBlobClient(blobName);
                    using (var stream = file.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, true);
                    }

                    uploadFile.Url = blobClient.Uri.ToString();
                }
                catch (Exception ex)
                {
                    uploadFile.Exito = false;
                    uploadFile.Error = ex.Message;
                }
                finally
                {
                    uploadResults.Add(uploadFile);
                }
            }
            return Ok(uploadResults);
        }
    }
}
