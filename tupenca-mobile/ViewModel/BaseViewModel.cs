using CommunityToolkit.Mvvm.ComponentModel;
using tupenca_mobile.Services;

namespace MonkeyFinder.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    RestService restService;
    public BaseViewModel(RestService restService)
    {
        this.restService = restService;
        this.restService.LoginSuccesfull += HandleConnection;
        this.restService.LoginTimeOut += HandleDisconnection;

    }
    [ObservableProperty]
    bool isBusy = false;

    [ObservableProperty]
    string title;

    async void HandleConnection(object sender, EventArgs a)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//list");
        });
    }

    async void HandleDisconnection(object sender, EventArgs a)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.GoToAsync("//main");
        });
    }
}
