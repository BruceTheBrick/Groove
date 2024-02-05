using Groove.Core;

namespace Groove.Services;

public class NavigationService : INavigationService
{
    private readonly IUriParsingService _uriParsingService;
    private readonly INavigationUtilityService _navigationUtilityService;
    private readonly IPageResolver _pageResolver;
    
    private const int MinimumNavigationDelay = 150;
    private DateTime _lastNavigationEvent;
    
    public NavigationService(
        IUriParsingService uriParsingService,
        INavigationUtilityService navigationUtilityService,
        IPageResolver pageResolver)
    {
        _uriParsingService = uriParsingService;
        _navigationUtilityService = navigationUtilityService;
        _pageResolver = pageResolver;
    }

    public Task<INavigationResponse> HandleNavigation(
        string uri, 
        INavigationParameters? parameters = null,
        bool isModal = false)
    {
        try
        {
            if (_uriParsingService.IsAbsoluteUri(uri))
            {
                return PerformAbsoluteNavigation(uri, parameters, isModal);
            }

            return PerformRelativeNavigation(uri, parameters, isModal);
        }
        catch (Exception ex)
        {
            return Task.FromResult(NavigationResponse.Failure(ex));
        }
    }

    private async Task<INavigationResponse> PerformAbsoluteNavigation(
        string uri,
        INavigationParameters? parameters,
        bool isModal)
    {
        var pages = _uriParsingService.ParsePages(uri);
        SetMainPage(pages.Pop());
        foreach (var page in pages)
        {
            var pageAndViewModel = _pageResolver.GetWiredPage(page);
            await PushPageToNavigationStack(pageAndViewModel, parameters, isModal);
        }

        return NavigationResponse.Success();
    }

    private async Task<INavigationResponse> PerformRelativeNavigation(
        string uri,
        INavigationParameters? parameters,
        bool isModal)
    {
        var pages = _uriParsingService.ParsePages(uri);
        SetMainPageIfNull(pages);
        while (pages.Count != 0)
        {
            var page = pages.Pop();
            var pageAndViewModel = _pageResolver.GetWiredPage(page);
            await PushPageToNavigationStack(pageAndViewModel, parameters, isModal);
        }

        return NavigationResponse.Success();
    }

    private async Task PushPageToNavigationStack(Page page, INavigationParameters? parameters, bool isModal)
    {
        await DelayForNavigationEvent();
        if (isModal)
        {
            await PushModalPage(page, parameters);
            return;
        }
        
        await PushPage(page, parameters);
    }

    private async Task DelayForNavigationEvent()
    {
        if (_lastNavigationEvent == DateTime.MinValue)
        {
            return;
        }

        var delay = (DateTime.Now - _lastNavigationEvent).TotalMilliseconds;
        await Task.Delay((int)Math.Max(MinimumNavigationDelay, delay));
        _lastNavigationEvent = DateTime.Now;
    }

    private void SetMainPageIfNull(Stack<string> pages)
    {
        if (_navigationUtilityService.IsMainPageSet())
        {
            return;
        }

        SetMainPage(pages.Pop());
    }

    private void SetMainPage(string mainPage)
    {
        var page = _pageResolver.GetWiredPage(mainPage);
        Application.Current!.MainPage = new NavigationPage(page);
        _lastNavigationEvent = DateTime.Now;
    }
    
    private async Task PushModalPage(Page page, INavigationParameters? parameters)
    {
        await GetMainPageNavigation().PushModalAsync(page, true);
    }

    private async Task PushPage(Page page, INavigationParameters? parameters)
    {
        await GetMainPageNavigation().PushAsync(page, true);
    }

    private INavigation GetMainPageNavigation()
    {
        return Application.Current!.MainPage!.Navigation;
    }
}