using Groove.Services;

namespace Groove;

public static class Groove
{
    public static MauiAppBuilder UseGroove(this MauiAppBuilder builder)
    {
        builder.RegisterGrooveServices()
            .RegisterGroovePages();
        return builder;
    }

    internal static MauiAppBuilder RegisterGrooveServices(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<IPageResolver, PageResolver>();
        builder.Services.AddTransient<INavigationService, NavigationService>();
        builder.Services.AddTransient<INavigationUtilityService, NavigationUtilityService>();
        builder.Services.AddTransient<IUriParsingService, UriParsingService>();
        return builder;
    }
    
    internal static MauiAppBuilder RegisterGroovePages(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<NavigationPage>();
        return builder;
    }
}