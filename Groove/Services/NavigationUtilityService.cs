using Groove.Core;

namespace Groove.Services;

public class NavigationUtilityService : INavigationUtilityService
{
    public PageType GetMainPageType()
    {
        var mainPage = GetMainPage();
        return GetPageType(mainPage);
    }

    public PageType GetParentPageType(Page page)
    {
        //TODO Complete
        return PageType.None;
    }

    public PageType GetPageType(Page page)
    {
        return page switch
        {
            NavigationPage => PageType.NavigationPage,
            TabbedPage => PageType.TabbedPage,
            _ => PageType.None
        };
    }

    public bool IsMainPageSet()
    {
        return Application.Current?.MainPage != null;
    }

    public Page? GetPageFromPageType(PageType pageType)
    {
        return pageType switch
        {
            PageType.NavigationPage => new NavigationPage(),
            PageType.TabbedPage => new TabbedPage(),
            _ => null
        };
    }

    private Page GetMainPage()
    {
        return Application.Current!.MainPage!;
    }
}