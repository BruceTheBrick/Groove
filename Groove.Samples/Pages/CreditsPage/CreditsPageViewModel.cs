using CommunityToolkit.Mvvm.Input;
using Groove.Services;

namespace Groove.Samples.Pages;

public partial class CreditsPageViewModel : BasePageViewModel
{
    public CreditsPageViewModel(INavigationService navigationService) 
        : base(navigationService)
    {
        Title = "This is the Credits Page!";
    }

    [RelayCommand]
    private Task GoBack()
    {
        return Application.Current.MainPage.Navigation.PopAsync();
    }
}