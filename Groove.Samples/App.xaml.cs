using Groove.Samples.Pages.HomePage;
using Groove.Services;

namespace Groove.Samples;

public partial class App
{
    public App(IGrooveNavigationService navigationService)
    {
        InitializeComponent();
        navigationService.Navigate($"/{nameof(HomePage)}");
    }
}