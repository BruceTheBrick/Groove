namespace Groove.Core;

public class NavigationResponse(bool isSuccessful, Exception exception) : INavigationResponse
{
    public bool IsSuccessful { get; } = isSuccessful;
    public Exception Exception { get; } = exception;
}