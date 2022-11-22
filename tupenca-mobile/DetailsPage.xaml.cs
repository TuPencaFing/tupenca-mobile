using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class DetailsPage : ContentPage
{
    public DetailsPage(PencaCompartidaDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        var vm = this.BindingContext as PencaCompartidaDetailsViewModel;
        if (vm == null) return;
        vm.checkNotifiaction();
    }

}