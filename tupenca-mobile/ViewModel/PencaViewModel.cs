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
        bool isLoggedIn = false;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        public ObservableCollection<PencaCompartidaDto> PencasCompartidas { get; } = new();
        public ObservableCollection<EventoPrediccionDto> ProximosEventos { get; } = new();

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

        }

        async Task GetProximosEventosAsync()
        {

            try
            {
                var eventos = await restService.getEventosProximos();

                if (eventos.Count != 0)
                    ProximosEventos.Clear();

                foreach (var evento in eventos)
                    ProximosEventos.Add(evento);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get eventos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }

        }

        [RelayCommand]
        async Task LoginAsync(object args)
        {

            try
            {
                await restService.Login(username, password);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to login: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
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
        async Task GoToDetails(PencaCompartidaDto penca)
        {
            if (penca == null)
                return;

            await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"PencaCompartidaDto", penca }
        });
        }
        void HandleConnection(object sender, EventArgs a)
        {
            GetPencasCompartidasAsync();
            GetProximosEventosAsync();
        }


    }
}
