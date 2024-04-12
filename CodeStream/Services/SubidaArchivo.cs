using Microsoft.AspNetCore.Components.Forms;

namespace CodeStream.Services
{
    public class SubidaArchivo : ISubidaArchivo
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public SubidaArchivo(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public bool BorrarArchivo(string nombreArchivo)
        {
            try
            {
                var path = $"{_webHostEnvironment.WebRootPath}\\{nombreArchivo}";
                if(File.Exists(path))
                {
                    File.Delete(path);
                    return true ;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<string> SubirArchivo(IBrowserFile archivo)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(archivo.Name);
                var fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                var folderDirectory = $"{_webHostEnvironment.WebRootPath}\\CourseImages";
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "CourseImages", fileName);

                var memoryStream = new MemoryStream();
                await archivo.OpenReadStream(5000000).CopyToAsync(memoryStream);

                if(!Directory.Exists(folderDirectory))
                {
                    Directory.CreateDirectory(folderDirectory);
                }

                await using(var fs = new FileStream(path, FileMode.Create, FileAccess.Write)) {
                    
                    memoryStream.WriteTo(fs);
                }

                var url = $"{_configuration.GetValue<string>("ServerUrl")}";
                var fullPath = $"{url}CourseImages/{fileName}";
                return fullPath;
            }
            catch (Exception )
            {

                throw;
            }
        }
    }
}
