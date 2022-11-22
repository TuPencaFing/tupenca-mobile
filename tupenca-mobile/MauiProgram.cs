using tupenca_mobile.Services;
using tupenca_mobile.ViewModel;

namespace tupenca_mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Bold.ttf", "OpenSansBold");
                fonts.AddFont("Sitka.ttc", "Sitka");
            });

        builder.Services.AddSingleton<RestService>();
        builder.Services.AddSingleton<PencaViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ListPencaPage>();
        builder.Services.AddSingleton<ProfilePage>();
        builder.Services.AddTransient<ProximosEventosPage>();
        builder.Services.AddScoped<PencaCompartidaDetailsViewModel>();
        builder.Services.AddTransient<DetailsPage>();

        return builder.Build();
	}
}
