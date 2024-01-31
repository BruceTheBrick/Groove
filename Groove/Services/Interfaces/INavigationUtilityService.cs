using Groove.Core;

namespace Groove.Services;

public interface INavigationUtilityService
{
    public PageType GetMainPageType();
    public PageType GetParentPageType(Page page);
    public PageType GetPageType(Page page);
    public bool IsMainPageSet();
    public Page? GetPageFromPageType(PageType pageType);
}