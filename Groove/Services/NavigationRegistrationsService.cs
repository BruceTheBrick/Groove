namespace Groove.Services;

public class NavigationRegistrationsService : INavigationRegistrationsService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigationRegistrationsService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public Page GetPageByName(string name)
    {
        var registration = NavigationRegistrations.GetRegistrationByName(name);
        return (Page)_serviceProvider.GetService(registration.Page);
    }

    public object GetPageViewModelByName(string name)
    {
        var registration = NavigationRegistrations.GetByPageViewModelName(name);
        return _serviceProvider.GetService(registration.PageViewModel);
    }
}