using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class ListPencaPage : ContentPage
{
	public ListPencaPage(PencaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

