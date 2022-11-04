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
using tupenca_back.Model;

namespace tupenca_mobile.Services
{
    public class RestService 
    {
        public EventHandler LoginTimeOut;
        public EventHandler LoginSuccesfull;
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        private string accessToken;
        public List<PencaCompartida> userList { get; private set; }

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
        public async Task<List<PencaCompartida>> RefreshDataAsync()
        {
            userList = new List<PencaCompartida>();

            Uri uri = new Uri(string.Format("https://10.0.2.2:7028/api/pencas-compartidas", string.Empty));
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

        public async Task Login(string username, string password)
        {
            accessToken = null;

            Uri uri = new Uri(string.Format("https://10.0.2.2:7028/api/usuarios/login", string.Empty));
            try
            {
                string json = JsonSerializer.Serialize<LoginRequest>(new LoginRequest { email = username, password = password}, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    accessToken = await response.Content.ReadAsStringAsync();
                    var je = JObject.Parse(accessToken);
                    accessToken = je["token"].ToString();
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
