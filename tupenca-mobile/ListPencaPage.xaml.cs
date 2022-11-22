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
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var vm = this.BindingContext as PencaViewModel;
        if (vm == null) return;
        vm.checkNotifiaction();
    }
}

