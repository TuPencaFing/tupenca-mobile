using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_mobile.Model;
using tupenca_mobile.Model.Dto;
using tupenca_mobile.Services;

namespace tupenca_mobile.ViewModel
{
    public partial class PencaViewModel : BaseViewModel
    {
        public string _deviceToken;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        UsuarioDto user;
        [ObservableProperty]
        bool userHasEmpresas = false;
        public ObservableCollection<PencaCompartidaDto> PencasCompartidas { get; } = new();


        RestService restService;

        public PencaViewModel(RestService restService) :base(restService)
        {
            this.restService = restService;
            this.restService.LoginSuccesfull += HandleConnection;
        }

        async Task GetPencasCompartidasAsync()
        {

            try
            {
                IsBusy = true;
                var pencasCompartidas = await restService.RefreshDataAsync();

                if (pencasCompartidas.Count != 0)
                    PencasCompartidas.Clear();

                foreach (var penca in pencasCompartidas)
                    PencasCompartidas.Add(penca);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get pencas: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task RegisterUserToken()
        {

            try
            {
                IsBusy = true;
                await restService.RegisterToken(_deviceToken);


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get pencas: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }

        async Task GetUserInformation()
        {

            try
            {
                IsBusy = true;
                User = await restService.getUserInformation();
                if(User.Empresas != null && user.Empresas.Count>0)
                {
                    UserHasEmpresas = true;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get pencas: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;

            }

        }



        [RelayCommand]
        async Task LoginAsync(object args)
        {

            try
            {
                IsBusy = true;
                await restService.Login(username, password);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to login: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
            //{

            //    var pushNotificationRequest = new PushNotificationRequest
            //    {
            //        notification = new NotificationMessageBody
            //        {
            //            title = "Notification Title",
            //            body = "Notification body"
            //        },
            //        registration_ids = new List<string> { _deviceToken }
            //    };

            //    string url = "https://fcm.googleapis.com/fcm/send";

            //    using (var client = new HttpClient())
            //    {
            //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("key", "=" + "AAAAwaa27z8:APA91bH8uO27ydtA3_4qLGAGWfzTv_xFofWvMab4WE5vgtce9mCZ4ZEN2JaEsrka9L3WnVlXDirlDTWXEZmNxcZ-vE2hGLFqwgaF7UM28OfnlUdZztUVM3_5dAheKo9AuXeM_Ct69PA9");

            //        string serializeRequest = JsonConvert.SerializeObject(pushNotificationRequest);
            //        var response = await client.PostAsync(url, new StringContent(serializeRequest, Encoding.UTF8, "application/json"));
            //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            await App.Current.MainPage.DisplayAlert("Notification sent", "notification sent", "OK");
            //        }
            //    }
            //}
        }

        [RelayCommand]
        async Task GoToRulesAsync(object args)
        {

            try
            {
                await Shell.Current.GoToAsync("rulesPage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to login: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task LogoutAsync(object args)
        {

            try
            {
                Username = null;
                Password = null;
                await Shell.Current.GoToAsync("//main");
                UserHasEmpresas = false;
                PencasCompartidas.Clear();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        async Task GoToDetails(PencaCompartidaDto penca)
        {
            if (penca == null)
                return;

            //    await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
            //{
            //    {"PencaCompartidaDto", penca }
            //});
            //var stringPencaDto = Newtonsoft.Json.JsonConvert.SerializeObject(penca);
            var a = Shell.Current.CurrentState.Location.ToString();
            await Shell.Current.GoToAsync($"//proximosEventos?PencaCompartidaId={penca.Id}&Title={penca.Title}&Pozo={penca.Pozo}&Costo={penca.CostEntry}&Image={penca.Image}");
        }
        void HandleConnection(object sender, EventArgs a)
        {
            GetPencasCompartidasAsync();
            GetUserInformation();
            RegisterUserToken();
        }


    }
}
