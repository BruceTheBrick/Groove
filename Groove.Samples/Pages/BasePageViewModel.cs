using CommunityToolkit.Mvvm.ComponentModel;
using Groove.Services;

namespace Groove.Samples.Pages;

public partial class BasePageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;

    public BasePageViewModel(IGrooveNavigationService navigationService)
    {
        NavigationService = navigationService;
    }

    protected IGrooveNavigationService NavigationService { get; }
}