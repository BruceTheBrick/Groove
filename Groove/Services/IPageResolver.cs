namespace Groove.Services;

public interface IPageResolver
{
    public Page GetPage<TPage>() where TPage : Page;
    public Page GetPage(string pageName);
    public object GetPageViewModel<TPageViewModel>() where TPageViewModel : new();
    public object GetPageViewModel(string pageViewModelName);
}