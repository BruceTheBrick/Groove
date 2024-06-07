namespace Groove.Core;

public interface IInitializeAsync
{
    public Task InitializeAsync(INavigationParameters parameters);
}