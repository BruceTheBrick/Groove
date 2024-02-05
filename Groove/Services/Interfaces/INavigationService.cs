using Groove.Core;

namespace Groove.Services;

public interface INavigationService
{
    public Task<INavigationResponse> HandleNavigation(string uri, INavigationParameters? parameters, bool isModal = false);
}