using IT_Desarrollo_Front.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace IT_Desarrollo_Front.Services
{
    public class Servicio_API : IServicio_API
    {
        public async Task<LoginResponse> PostLogin(string jsonData)
        {
            string urlLogin = Urls.usuario + "/login";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlLogin, content);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        LoginResponse login = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
                        var jwt = login.mensaje.Replace("bearer ", string.Empty);
                        var handler = new JwtSecurityTokenHandler();
                        var token = handler.ReadJwtToken(jwt);
                        var roleClaim = token.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;
                        login.rol = roleClaim;
                        return login;

                    }
                    else
                    {
                        LoginResponse login = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);
                        return login;
                    }
                }
                catch (HttpRequestException ex)
                {
                    return null;
                }
            }
        }
    }
}
