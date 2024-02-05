using Groove.Core;

namespace Groove.Services;

public interface IGrooveNavigationService
{
    public Task<INavigationResponse> Navigate(string uri);
    public Task<INavigationResponse> Navigate(string uri, INavigationParameters parameters);
    public Task<INavigationResponse> Navigate(string uri, INavigationParameters parameters, bool isModal);
    public Task<INavigationResponse> Navigate(string uri, bool isModal);
}