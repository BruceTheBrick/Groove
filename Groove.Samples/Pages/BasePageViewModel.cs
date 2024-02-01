using CommunityToolkit.Mvvm.ComponentModel;
using Groove.Services;

namespace Groove.Samples.Pages;

public partial class BasePageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;

    public BasePageViewModel(INavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    protected INavigationService NavigationService { get; }
}