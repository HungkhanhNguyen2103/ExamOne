using ExamOne.Models;
using SharpCompress.Common;
using System.Buffers.Text;

namespace ExamOne.Helper
{
    public interface IImageManagement
    {
        Task<ResponderData<string>> UpdateAvatar(string createBy, string avatar);
    }

    public class ImageManagement : IImageManagement
    {
        private readonly IHttpContextAccessor _http;
        private readonly IWebHostEnvironment _env;
        public ImageManagement(IWebHostEnvironment env, IHttpContextAccessor http) 
        {
            _env = env;
            _http = http;
        }

        public async Task<ResponderData<string>> UpdateAvatar(string createBy,string avatar)
        {
            var domain = GetDomain();
            var result = new ResponderData<string>();
            if(string.IsNullOrEmpty(createBy) || string.IsNullOrEmpty(avatar))
            {
                result.Message = "Thông tin không hợp lệ";
                return result;
            }
            try
            {
                if (avatar.Contains(","))
                {
                    avatar = avatar.Split(",")[1];
                }
                var fileBytes = Convert.FromBase64String(avatar);
                string uploadPath = Path.Combine(_env.WebRootPath, "uploads", "avatar");
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                string fileName = createBy + ".png";
                string filePath = Path.Combine(uploadPath, fileName);

                //Path.Combine(Path.DirectorySeparatorChar.ToString(), "uploads", "avatar", fileName);
                var filePathLocal = $"{domain}/uploads/avatar/{fileName}"; 
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                await File.WriteAllBytesAsync(filePath, fileBytes);
                result.Message = "Thành công";
                result.Data = filePathLocal;
                result.IsSuccess = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        private string GetDomain()
        {
            var req = _http.HttpContext?.Request;
            if (req == null) return string.Empty;

            return $"{req.Scheme}://{req.Host}";
        }
    }

}
