using Groove.Core;

namespace Groove.Services;

public class NavigationService : INavigationService
{
    private readonly IUriParsingService _uriParsingService;
    private readonly INavigationUtilityService _navigationUtilityService;
    private readonly IPageResolver _pageResolver;
    private DateTime _lastNavigationEvent;
    private int _minimumNavigationDelay = 150;
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
            SetMainPage(uri);
            var pages = _uriParsingService.ParsePages(uri);
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

    private Page CreatePageAndViewModel(string pageName)
    {
        var page = _pageResolver.GetPage(pageName);
        var viewModel = _pageResolver.GetPageViewModel($"{pageName}ViewModel");
        page.BindingContext = viewModel;
        return page;
    }

    private async Task<INavigationResponse> PerformRelativeNavigation(
        string uri, 
        INavigationParameters? parameters,
        bool isModal)
    {
        try
        {
            SetMainPageIfNull(uri);
            var pages = _uriParsingService.ParsePages(uri);
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

    private async Task PushPageToNavigationStack(Page page)
    {
        await DelayForNavigationEvent();
        await Application.Current.NavigationProxy.PushAsync(page);
    }

    private async Task DelayForNavigationEvent()
    {
        if (_lastNavigationEvent == null)
        {
            return;
        }

        var delay = (DateTime.Now - _lastNavigationEvent).TotalMilliseconds;
        await Task.Delay((int)Math.Max(_minimumNavigationDelay, delay));
        _lastNavigationEvent = DateTime.Now;
    }

    private void SetMainPageIfNull(string uri)
    {
        if (_navigationUtilityService.IsMainPageSet())
        {
            return;
        }

        SetMainPage(uri);
    }

    private void SetMainPage(string uri)
    {
        var firstPageName = _uriParsingService.GetFirstPage(uri);
        var pageType = _pageResolver.GetPage(firstPageName);
        var firstPageType = _navigationUtilityService.GetPageType(pageType);
        if (firstPageType == PageType.None)
        { 
            throw new InvalidMainPageException();
        }

        Application.Current!.MainPage = _navigationUtilityService.GetPageFromPageType(firstPageType);
    }
}