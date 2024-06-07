namespace Groove.Core;

public interface IOnNavigatedToAsync
{
    public Task OnNavigatedToAsync(INavigationParameters parameters);
}