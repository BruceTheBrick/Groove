using Groove.Core;

namespace Groove.Services;

public class LifecycleEventHandler : ILifecycleEventHandler
{
    public void Initialize(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IInitialize initializer)
        {
            initializer.Initialize(parameters);
        }
    }

    public Task InitializeAsync(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IInitializeAsync initializerAsync)
        {
            return initializerAsync.InitializeAsync(parameters);
        }

        return Task.CompletedTask;
    }

    public void OnNavigatedTo(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnNavigatedTo onNavigatedTo)
        {
            onNavigatedTo.OnNavigatedTo(parameters);
        }
    }

    public Task OnNavigatedToAsync(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnNavigatedToAsync onNavigatedToAsync)
        {
            return onNavigatedToAsync.OnNavigatedToAsync(parameters);
        }
        
        return Task.CompletedTask;
    }

    public void OnAppearing(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnAppearing onAppearing)
        {
            onAppearing.OnAppearing(parameters);
        }
    }

    public Task OnAppearingAsync(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnAppearingAsync onAppearingAsync)
        {
            return onAppearingAsync.OnAppearingAsync(parameters);
        }

        return Task.CompletedTask;
    }

    public void OnNavigatedFrom(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnNavigatedFrom onNavigatedFrom)
        {
            onNavigatedFrom.OnNavigatedFrom(parameters);
        }
    }

    public Task OnNavigatedFromAsync(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnNavigatedFromAsync onNavigatedFromAsync)
        {
            return onNavigatedFromAsync.OnNavigatedFromAsync(parameters);
        }

        return Task.CompletedTask;
    }

    public void OnDisappearing(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnDisappearing onDisappearing)
        {
            onDisappearing.OnDisappearing(parameters);
        }
    }

    public Task OnDisappearingAsync(Page page, INavigationParameters parameters)
    {
        if (page.BindingContext is IOnDisappearingAsync onDisappearingAsync)
        {
            onDisappearingAsync.OnDisappearingAsync(parameters);
        }
        
        return Task.CompletedTask;
    }
}