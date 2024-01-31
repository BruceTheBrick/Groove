namespace Groove.Core;

public interface INavigationResponse
{
    public bool IsSuccessful { get; }
    public Exception Exception { get; }
}