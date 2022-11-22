namespace tupenca_mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        //Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));

    }

    //protected override void OnNavigated(ShellNavigatedEventArgs args)
    //{
    //    string location = args.Current?.Location?.ToString();

    //    //if (location is $"//{nameof(StoreListPage)}")
    //    //{
    //    //    ((StoreListPage)Shell.Current.CurrentPage)._viewModel.StoreListViewModelConstructorCode();
    //    //}

    //    base.OnNavigated(args);
    //}
}
