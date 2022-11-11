using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class MainPage : ContentPage
{
	private string _deviceToken;
	public MainPage(PencaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		
		if (Preferences.ContainsKey("DeviceToken"))
		{
			_deviceToken = Preferences.Get("DeviceToken","");
			viewModel._deviceToken = _deviceToken;
		}

        if (Preferences.ContainsKey("pencaId"))
        {
            string id = Preferences.Get("pencaId", "");
            if (id == "1")
            {
                //AppShell.Current.GoToAsync(nameof(NewPage1));
            }
            if (id == "2")
            {
                //AppShell.Current.GoToAsync(nameof(NewPage2));
            }
            Preferences.Remove("pencaId");
        }
    }

	private void OnLogin(object sender, EventArgs e)
	{
		int a = 0;

    }
}

