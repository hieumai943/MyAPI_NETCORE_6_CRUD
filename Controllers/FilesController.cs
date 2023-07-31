using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace MyAPINetCore6.Controllers
{
   
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationtoken)
        {
            var result = await WriteFile(file);
            return Ok(result);
        }
        internal async Task<string> WriteFile(IFormFile file)
        {
            string filename = "";
            try
            {
                // Lấy phần mở rộng của tên tệp từ tên gốc
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];

                // Tạo tên mới cho tệp bằng cách kết hợp ticks của thời gian hiện tại với phần mở rộng tệp
                filename = DateTime.Now.Ticks.ToString() + extension;
                // Đường dẫn thư mục nơi tệp sẽ được lưu trữ
                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");
                // Kiểm tra xem thư mục lưu trữ có tồn tại hay không; nếu không, tạo thư mục mới
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                // Tạo đường dẫn hoàn chỉnh tới tệp, kết hợp đường dẫn thư mục và tên tệp mới
                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);

                // Mở một luồng tệp mới(FileStream) với chế độ tạo mới(FileMode.Create)
                 // và sao chép dữ liệu từ `IFormFile` đã nhận được vào luồng tệp này
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex) { }
            return filename;
        }
        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile(string filename)
        {
            // Đường dẫn thư mục nơi tệp sẽ được lưu trữ
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
            var provider = new FileExtensionContentTypeProvider(); // xac dinh loai noi dung dua tren phan mo rong
            // Nếu không thể xác định loại nội dung, giá trị mặc định "application/octet-stream" sẽ được sử dụng.

            if (!provider.TryGetContentType(filepath,out var contentType)){
                contentType = "application/octet-stream";
            }
            //Đoạn mã này đọc toàn bộ nội dung của tệp thành một mảng bytes
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes,contentType,Path.GetFileName(filepath)); 

        }
    }
}
