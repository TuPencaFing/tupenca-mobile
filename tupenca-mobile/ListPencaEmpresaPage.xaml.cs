using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class ListPencaEmpresaPage : ContentPage
{
	public ListPencaEmpresaPage(PencaViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}

