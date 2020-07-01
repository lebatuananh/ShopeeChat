using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopeeChat.CoreAPI.RestClientShopee.Repositories.Interfaces;
using System.Threading.Tasks;

namespace ShopeeChat.WebApi.Controllers
{
    public class UploadController : V1Controller
    {
        private readonly IUploadRepository _uploadRepository;

        public UploadController(IUploadRepository uploadRepository)
        {
            _uploadRepository = uploadRepository;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile formFile)
        {
            var result = await _uploadRepository.UploadImage(formFile);
            return Ok(result);
        }
    }
}