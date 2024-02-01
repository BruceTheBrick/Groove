using CommunityToolkit.Maui;
using Groove.Samples.Pages;
using Groove.Samples.Pages.HomePage;
using Groove.Services;
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
        builder.Services.AddPageForNavigation<HomePage, HomePageViewModel>();
        builder.Services.AddPageForNavigation<CreditsPage, CreditsPageViewModel>();
        return builder;
    }
}