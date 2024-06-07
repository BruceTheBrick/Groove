using Groove.Core;

namespace Groove.Services;

public interface ILifecycleEventHandler
{
    public void Initialize(Page page, INavigationParameters parameters);
    public Task InitializeAsync(Page page, INavigationParameters parameters);
    public void OnNavigatedTo(Page page, INavigationParameters parameters);
    public Task OnNavigatedToAsync(Page page, INavigationParameters parameters);
    public void OnAppearing(Page page, INavigationParameters parameters);
    public Task OnAppearingAsync(Page page, INavigationParameters parameters);
    public void OnNavigatedFrom(Page page, INavigationParameters parameters);
    public Task OnNavigatedFromAsync(Page page, INavigationParameters parameters);
    public void OnDisappearing(Page page, INavigationParameters parameters);
    public Task OnDisappearingAsync(Page page, INavigationParameters parameters);
}