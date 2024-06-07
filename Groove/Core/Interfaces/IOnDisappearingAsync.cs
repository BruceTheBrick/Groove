namespace Groove.Core;

public interface IOnDisappearingAsync
{
    public Task OnDisappearingAsync(INavigationParameters parameters);
}