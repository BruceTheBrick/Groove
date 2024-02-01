using Groove.Core;

namespace Groove.Services;

public class NavigationService : INavigationService
{
    private readonly IUriParsingService _uriParsingService;
    private readonly INavigationUtilityService _navigationUtilityService;
    private readonly IPageResolver _pageResolver;
    private const int _minimumNavigationDelay = 150;
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
    public Task<INavigationResponse> Navigate(string uri)
    {
        return HandleNavigation(uri);
    }

    public Task<INavigationResponse> Navigate(string uri, INavigationParameters parameters)
    {
        return HandleNavigation(uri, parameters);
    }

    public Task<INavigationResponse> Navigate(string uri, INavigationParameters parameters, bool isModal)
    {
        return HandleNavigation(uri, parameters, isModal);
    }

    private Task<INavigationResponse> HandleNavigation(
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
            var navigationResponse = new NavigationResponse(false, ex);
            return Task.FromResult<INavigationResponse>(navigationResponse);
        }
    }

    private async Task<INavigationResponse> PerformAbsoluteNavigation(
        string uri, 
        INavigationParameters? parameters,
        bool isModal)
    {
        try
        {
            var pages = _uriParsingService.ParsePages(uri);
            SetMainPage(pages.Pop());
            foreach (var page in pages)
            {
                var pageAndViewModel = CreatePageAndViewModel(page);
                await PushPageToNavigationStack(pageAndViewModel);
            }

            return new NavigationResponse(true, null);
        }
        catch (Exception e)
        {
            return new NavigationResponse(false, e);
        }
    }

    private async Task<INavigationResponse> PerformRelativeNavigation(
        string uri, 
        INavigationParameters? parameters,
        bool isModal)
    {
        try
        {
            var pages = _uriParsingService.ParsePages(uri);
            SetMainPageIfNull(pages);
            while (pages.Any())
            {
                var page = pages.Pop();
                var pageAndViewModel = CreatePageAndViewModel(page);
                await PushPageToNavigationStack(pageAndViewModel);
            }
      
            return new NavigationResponse(true, null);
        }
        catch (Exception e)
        {
            return new NavigationResponse(false, e);
        }
    }
    
    private Page CreatePageAndViewModel(string pageName)
    {
        var page = _pageResolver.GetPage(pageName);
        var viewModel = _pageResolver.GetPageViewModel($"{pageName}ViewModel");
        page.BindingContext = viewModel;
        return page;
    }

    private async Task PushPageToNavigationStack(Page page)
    {
        await DelayForNavigationEvent();
        await Application.Current.MainPage.NavigationProxy.PushAsync(page, true);
    }

    private async Task DelayForNavigationEvent()
    {
        if (_lastNavigationEvent == DateTime.MinValue)
        {
            return;
        }

        var delay = (DateTime.Now - _lastNavigationEvent).TotalMilliseconds;
        await Task.Delay((int)Math.Max(_minimumNavigationDelay, delay));
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
        var page = CreatePageAndViewModel(mainPage);
        Application.Current!.MainPage = new NavigationPage(page);
        _lastNavigationEvent = DateTime.Now;
    }
}