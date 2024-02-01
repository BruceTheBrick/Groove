namespace Groove.Services;

public class UriParsingService : IUriParsingService
{
    private string _uriSeparator = "/";
    public Stack<string> ParsePages(string uri)
    {
        var pages = new Stack<string>();
        var uriParts = uri.Split(_uriSeparator);
        foreach (var part in uriParts)
        {
            if (string.IsNullOrEmpty(part) || part.Equals(_uriSeparator))
            {
                continue;
            }

            pages.Push(part);
        }

        return pages;
    }

    public string GetFirstPage(string uri)
    {
        var uriParts = uri.Split(_uriSeparator);
        var firstPageName = uriParts.FirstOrDefault(x => !x.Equals(_uriSeparator));
        return firstPageName;
    }

    public bool IsAbsoluteUri(string uri)
    {
        return uri.StartsWith(_uriSeparator);
    }
}