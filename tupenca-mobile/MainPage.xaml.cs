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
    }

	private void OnLogin(object sender, EventArgs e)
	{
		int a = 0;

    }
}

