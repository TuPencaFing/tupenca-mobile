using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class ProximosEventosPage : ContentPage
{
	public ProximosEventosPage(PencaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

