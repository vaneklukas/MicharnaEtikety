using Etikety.Interfaces;
using Etikety.Services;

namespace Etikety;

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
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<IApiService,ApiService>();
		builder.Services.AddTransient<IPrinterService,PrinterService>();
		

		var app = builder.Build();

		Services = app.Services;

		return app;
	}
	public static IServiceProvider Services { get; private set; }
}
