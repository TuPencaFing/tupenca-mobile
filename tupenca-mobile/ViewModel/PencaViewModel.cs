using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tupenca_back.Model;
using tupenca_mobile.Model;
using tupenca_mobile.Services;

namespace tupenca_mobile.ViewModel
{
    public partial class PencaViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isLoggedIn = false;

        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        public ObservableCollection<Penca> Usuarios { get; } = new();
        RestService restService;

        public PencaViewModel(RestService restService) :base(restService)
        {
            this.restService = restService;
            this.restService.LoginSuccesfull += HandleConnection;
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        {

            try
            {
                var users = await restService.RefreshDataAsync();

                if (users.Count != 0)
                    Usuarios.Clear();

                foreach (var monkey in users)
                    Usuarios.Add(monkey);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get users: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }

        }

        [RelayCommand]
        async Task LoginAsync(object args)
        {

            try
            {
               await restService.Login(username,password);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to login: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
        void HandleConnection(object sender, EventArgs a)
        {
            GetMonkeysAsync();

        }
    }
}
