using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using tupenca_mobile.Model;
using tupenca_mobile.Model.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace tupenca_mobile.Services
{
    public class RestService
    {
        public EventHandler LoginTimeOut;
        public EventHandler LoginSuccesfull;
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        private string accessToken;
        public List<PencaCompartidaDto> userList { get; private set; }
        public List<EventoPrediccionDto> eventoList { get; private set; }
        public int myId { get; private set; }

        public RestService()
        {
#if DEBUG
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            _client = new HttpClient(handler.GetPlatformMessageHandler());
#else
            _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        public async Task<List<PencaCompartidaDto>> RefreshDataAsync()
        {
            userList = new List<PencaCompartidaDto>();

            Uri uri = new Uri(string.Format("https://10.0.2.2:7028/api/pencas-compartidas/me", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    userList = JsonSerializer.Deserialize<List<PencaCompartidaDto>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return userList;
        }

        public async Task<PencaCompartidaDto> getPenca(int pencaId)
        {
            var user = new PencaCompartidaDto();

            Uri uri = new Uri(string.Format($"https://10.0.2.2:7028/api/pencas-compartidas/{pencaId}", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonSerializer.Deserialize<PencaCompartidaDto>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return user;
        }

        public async Task<List<EventoPrediccionDto>> getEventosProximos(int pencaid)
        {
            eventoList = new List<EventoPrediccionDto>();
            //CHANGEEEEE
            Uri uri = new Uri(string.Format($"https://10.0.2.2:7028/api/eventos/misproximos?penca={pencaid}", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    eventoList = JsonSerializer.Deserialize<List<EventoPrediccionDto>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return eventoList;
        }

        public async Task<List<UsuarioScoreDTO>> getUsersByPenca(int pencaid)
        {
            var usuarioScoreList = new List<UsuarioScoreDTO>();

            Uri uri = new Uri(string.Format($"https://10.0.2.2:7028/api/pencas-compartidas/{pencaid}/usuarios", string.Empty));
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    usuarioScoreList = JsonSerializer.Deserialize<List<UsuarioScoreDTO>>(content, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return usuarioScoreList;
        }

        public async Task Login(string username, string password)
        {
             accessToken = null;
            Uri uri = new Uri(string.Format("https://10.0.2.2:7028/api/usuarios/login", string.Empty));
            try
            {
                string json = JsonSerializer.Serialize<LoginRequest>(new LoginRequest { email = username, password = password }, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    accessToken = await response.Content.ReadAsStringAsync();
                    var je = JObject.Parse(accessToken);
                    accessToken = je["token"].ToString();
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(accessToken);
                    var tokenS = jsonToken as JwtSecurityToken;
                    myId = int.Parse(tokenS.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value);

                    _client.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", accessToken);
                    LoginSuccesfull?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    Debug.WriteLine($"not succesfull response");
                    await Shell.Current.DisplayAlert("Error!", response.ReasonPhrase, "OK");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

        public async Task ingresarPrediccion(int pencaId, int eventoId, PrediccionDto prediccion)
        {

            Uri uri = new Uri(string.Format($"https://10.0.2.2:7028/api/eventos/{eventoId}/prediccion?pencaId={pencaId}", string.Empty));
            try
            {
                if (prediccion.PuntajeEquipoLocal > prediccion.PuntajeEquipoVisitante)
                {
                    prediccion.resultado = TipoResultado.VictoriaEquipoLocal;
                }
                else if (prediccion.PuntajeEquipoLocal < prediccion.PuntajeEquipoVisitante)
                {
                    prediccion.resultado = TipoResultado.VictoriaEquipoVisitante;
                }
                else
                {
                    prediccion.resultado = TipoResultado.Empate;

                }

                string json = JsonSerializer.Serialize<PrediccionDto>(prediccion);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.DisplayAlert("Prediccion Ingresada!", "La prediccion a sido ingresada", "OK");
                }
                else
                {
                    Debug.WriteLine($"not succesfull response");
                    await Shell.Current.DisplayAlert("Error!", response.ReasonPhrase, "OK");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

        }
    }

    public class HttpsClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
            var handler = new CustomAndroidMessageHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalhost
        };
        return handler;
#else
     throw new PlatformNotSupportedException("Only Android and iOS supported.");
#endif
        }

#if ANDROID
        internal sealed class CustomAndroidMessageHandler : Xamarin.Android.Net.AndroidMessageHandler
        {
            protected override Javax.Net.Ssl.IHostnameVerifier GetSSLHostnameVerifier(Javax.Net.Ssl.HttpsURLConnection connection)
                => new CustomHostnameVerifier();

            private sealed class CustomHostnameVerifier : Java.Lang.Object, Javax.Net.Ssl.IHostnameVerifier
            {
                public bool Verify(string hostname, Javax.Net.Ssl.ISSLSession session)
                {
                    return Javax.Net.Ssl.HttpsURLConnection.DefaultHostnameVerifier.Verify(hostname, session) ||
                        hostname == "10.0.2.2" && session.PeerPrincipal?.Name == "CN=localhost";
                }
            }
        }
#elif IOS
    public bool IsHttpsLocalhost(NSUrlSessionHandler sender, string url, Security.SecTrust trust)
    {
        if (url.StartsWith("https://localhost"))
            return true;
        return false;
    }
#endif
    }
}
