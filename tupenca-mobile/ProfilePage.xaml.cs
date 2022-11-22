using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class ProfilePage : ContentPage
{
	private string _deviceToken;
	public ProfilePage(PencaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}

