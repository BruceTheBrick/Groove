namespace Groove.Services;

public interface INavigationRegistrationsService
{
    public Page GetPageByName(string name);
    public object GetPageViewModelByName(string name);
}