using Groove.Core;

namespace Groove.Services;

public static class NavigationRegistrations
{
    public static List<PagePageViewModelPair> Registrations = new ();
    
    public static IServiceCollection AddPageForNavigation<TPage, TPageViewModel>(this IServiceCollection serviceCollection, string name = "") 
        where TPage : Page 
        where TPageViewModel : class
    {
        var registration = new PagePageViewModelPair();
        name = string.IsNullOrWhiteSpace(name) ? typeof(TPage).Name : name;
        registration.Name = name;
        registration.Page = typeof(TPage);
        registration.PageViewModel = typeof(TPageViewModel);
        Registrations.Add(registration);

        serviceCollection.AddTransient<TPage>();
        serviceCollection.AddTransient<TPageViewModel>();

        return serviceCollection;
    }

    public static PagePageViewModelPair GetRegistrationByName(string name)
    {
        return Registrations.FirstOrDefault(x => x.Name == name);
    }

    public static PagePageViewModelPair GetByPageViewModelName(string name)
    {
        return Registrations.FirstOrDefault(x => x.PageViewModel.Name == name);
    }
}