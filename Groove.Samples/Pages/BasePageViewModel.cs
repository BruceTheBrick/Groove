using CommunityToolkit.Mvvm.ComponentModel;

namespace Groove.Samples.Pages;

public partial class BasePageViewModel : ObservableObject
{
    [ObservableProperty]
    private string _title;
}