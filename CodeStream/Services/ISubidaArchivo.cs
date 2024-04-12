using Microsoft.AspNetCore.Components.Forms;

namespace CodeStream.Services
{
    public interface ISubidaArchivo
    {
        Task<string> SubirArchivo(IBrowserFile archivo);
        bool BorrarArchivo(string nombreArchivo);
    }
}
