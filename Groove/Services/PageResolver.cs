namespace Groove.Services;

public class PageResolver : IPageResolver
{
    private IServiceProvider _serviceProvider;
    public PageResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public Page GetPage<TPage>() where TPage : Page
    {
        return _serviceProvider.GetService<TPage>();
    }

    public Page GetPage(string pageName)
    {
        return (Page)_serviceProvider.GetService(Type.GetType(pageName));
    }

    public object GetPageViewModel(string pageViewModelName)
    {
        return (object) _serviceProvider.GetService(Type.GetType(pageViewModelName));
    }

    public object GetPageViewModel<TPageViewModel>() where TPageViewModel : new()
    {
        return _serviceProvider.GetService<TPageViewModel>();
    }
}