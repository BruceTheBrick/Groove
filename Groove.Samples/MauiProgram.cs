using CommunityToolkit.Maui;
using Groove.Samples.Pages.HomePage;
using Microsoft.Extensions.Logging;

namespace Groove.Samples;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>()
            .UseGroove()
            .UseMauiCommunityToolkit()
            .RegisterPages()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        return builder.Build();
    }

    private static MauiAppBuilder RegisterPages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<HomePageViewModel>();
        return builder;
    }
}