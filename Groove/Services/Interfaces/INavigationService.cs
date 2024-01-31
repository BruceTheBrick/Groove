using Groove.Core;

namespace Groove.Services;

public interface INavigationService
{
    public Task<INavigationResponse> Navigate(string uri);
}