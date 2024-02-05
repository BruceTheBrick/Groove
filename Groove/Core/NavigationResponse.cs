namespace Groove.Core;

public class NavigationResponse : INavigationResponse
{
    public NavigationResponse(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
        Exception = null;
    }
    
    public NavigationResponse(bool isSuccessful, Exception exception)
    {
        IsSuccessful = isSuccessful;
        Exception = exception;
    }
    
    public bool IsSuccessful { get; }
    public Exception? Exception { get; }

    public static INavigationResponse Success()
    {
        return new NavigationResponse(true);
    }

    public static INavigationResponse Failure(Exception exception)
    {
        return new NavigationResponse(false, exception);
    }
}