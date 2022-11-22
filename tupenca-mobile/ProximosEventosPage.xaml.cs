using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class ProximosEventosPage : ContentPage
{
    PencaCompartidaDetailsViewModel viewModel;

    public ProximosEventosPage(PencaCompartidaDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        this.viewModel = viewModel;
        //// Get current page
        //var page = Navigation.NavigationStack.LastOrDefault();

        //// Load new page
        //Shell.Current.GoToAsync(nameof(ProximosEventosPage), false);

        //// Remove old page
        //Navigation.RemovePage(page);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var vm = this.BindingContext as PencaCompartidaDetailsViewModel;
        if (vm == null) return;
        vm.checkNotifiaction();
    }

}

