using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class MainPage : ContentPage
{
	public MainPage(PencaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private void OnLogin(object sender, EventArgs e)
	{
		int a = 0;

    }
}

