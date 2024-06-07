namespace Groove.Core;

public interface IOnNavigatedFromAsync
{
    public Task OnNavigatedFromAsync(INavigationParameters parameters);
}