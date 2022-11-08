using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public partial class DetailsPage : ContentPage
{
    PencaCompartidaDetailsViewModel viewModel;
    public DetailsPage(PencaCompartidaDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        viewModel.getUsuariosByPencaAsync();
    }
}