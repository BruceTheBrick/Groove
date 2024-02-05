using CommunityToolkit.Mvvm.Input;
using Groove.Services;

namespace Groove.Samples.Pages.HomePage;

public partial class HomePageViewModel : BasePageViewModel
{
    public HomePageViewModel(IGrooveNavigationService navigationService)
    :base(navigationService)
    {
        Title = "This is the title!";
    }

    [RelayCommand]
    private Task NavigateToCredits()
    {
        return NavigationService.Navigate(nameof(CreditsPage));
    }
}