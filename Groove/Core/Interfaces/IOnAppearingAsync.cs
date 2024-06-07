namespace Groove.Core;

public interface IOnAppearingAsync
{
    public Task OnAppearingAsync(INavigationParameters parameters);
}