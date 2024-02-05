using Groove.Core;

namespace Groove.Services;

public class GrooveNavigationService : IGrooveNavigationService
{
    private readonly INavigationService _navigationService;
    public GrooveNavigationService(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }
    
    public Task<INavigationResponse> Navigate(string uri)
    {
        return _navigationService.HandleNavigation(uri, null);
    }

    public Task<INavigationResponse> Navigate(string uri, INavigationParameters parameters)
    {
        return _navigationService.HandleNavigation(uri, parameters);
    }

    public Task<INavigationResponse> Navigate(string uri, INavigationParameters parameters, bool isModal)
    {
        return _navigationService.HandleNavigation(uri, parameters, isModal);
    }

    public Task<INavigationResponse> Navigate(string uri, bool isModal)
    {
        return _navigationService.HandleNavigation(uri, null, isModal);
    }
}