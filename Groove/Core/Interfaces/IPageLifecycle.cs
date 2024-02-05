namespace Groove.Core;

public interface IPageLifecycle
{
    public void OnNavigatedTo(INavigationParameters parameters);
    public void OnNavigatedFrom(INavigationParameters parameters);
    public void OnNavigatingFrom(INavigationParameters parameters);
    public void OnAppearing(INavigationParameters parameters);
    public void OnDisappearing(INavigationParameters parameters);
}