using IT_Desarrollo_Front.Models;

namespace IT_Desarrollo_Front.Services
{
    public interface IServicio_API
    {
        Task<LoginResponse> PostLogin(string jsonData);
    }
}
