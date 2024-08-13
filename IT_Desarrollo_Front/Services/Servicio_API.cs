using IT_Desarrollo_Front.Models;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
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
        public async Task<List<Usuarios>> GetUsuarios(string token)
        {
            string ulrUsuarios = Urls.usuario;

            string tokenLimpio = token.Replace("bearer ", "", StringComparison.OrdinalIgnoreCase);

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenLimpio);
                var respuesta = await client.GetAsync(ulrUsuarios);
                try
                {
                    respuesta.EnsureSuccessStatusCode();
                    var responseBody = await respuesta.Content.ReadAsStringAsync();

                    List<Usuarios> usuarios = JsonConvert.DeserializeObject<List<Usuarios>>(responseBody);

                    return usuarios;
                }
                catch (Exception ex)
                {
                    return null;

                }

            }


        }
        public async Task<List<Preguntas>> GetPreguntas()
        {
            string urlPreguntas = Urls.usuario + "/preguntas";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                var respuesta = await client.GetAsync(urlPreguntas);
                try
                {
                    respuesta.EnsureSuccessStatusCode();

                    var responseBody = await respuesta.Content.ReadAsStringAsync();

                    List<Preguntas> preguntas = JsonConvert.DeserializeObject<List<Preguntas>>(responseBody);

                    return preguntas;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
        public async Task<LoginResponse> PostRegistro(string jsonData)
        {
            string urlLogin = Urls.usuario;

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
                        LoginResponse registro = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                        return registro;

                    }
                    else
                    {
                        LoginResponse registro = JsonConvert.DeserializeObject<LoginResponse>(jsonResponse);

                        return registro;
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
