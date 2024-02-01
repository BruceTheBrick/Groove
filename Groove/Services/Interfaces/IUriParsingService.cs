namespace Groove.Services;

public interface IUriParsingService
{
    public Stack<string> ParsePages(string uri);
    public string GetFirstPage(string uri);
    public bool IsAbsoluteUri(string uri);
}