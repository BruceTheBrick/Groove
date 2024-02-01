namespace Groove.Services;

public class PageResolver : IPageResolver
{
    private IServiceProvider _serviceProvider;
    private INavigationRegistrationsService _navigationRegistrationsService;
    public PageResolver(IServiceProvider serviceProvider, INavigationRegistrationsService navigationRegistrationsService)
    {
        _serviceProvider = serviceProvider;
        _navigationRegistrationsService = navigationRegistrationsService;
    }
    public Page GetPage<TPage>() where TPage : Page
    {
        return _serviceProvider.GetService<TPage>();
    }

    public Page GetPage(string pageName)
    {
        return _navigationRegistrationsService.GetPageByName(pageName);
    }

    public object GetPageViewModel(string pageViewModelName)
    {
        return _navigationRegistrationsService.GetPageViewModelByName(pageViewModelName);
    }

    public object GetPageViewModel<TPageViewModel>() where TPageViewModel : new()
    {
        return _serviceProvider.GetService<TPageViewModel>();
    }
}