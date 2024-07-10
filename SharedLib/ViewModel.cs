using CommunityToolkit.Mvvm.ComponentModel;

namespace SharedLib;


public record BaseServices(
    INavigationService Navigator,
    IPageDialogService Dialogs
);

public abstract partial class ViewModel(BaseServices services) : ObservableObject, INavigationAware, IDestructible,  IPageLifecycleAware, IApplicationLifecycleAware
{
    [ObservableProperty] string? title;
    [ObservableProperty] bool isBusy;


    protected INavigationService Navigation => services.Navigator;
    protected IPageDialogService Dialogs => services.Dialogs;
    
    
    public virtual void OnNavigatedFrom(INavigationParameters parameters) {}
    public virtual void OnNavigatedTo(INavigationParameters parameters) {}
    public virtual void OnAppearing() {}
    public virtual void OnDisappearing()
    {
        this.Deactivate();
    }

    
    public void OnResume() => this.OnAppearing();
    public void OnSleep() => this.OnDisappearing();
    public virtual void Destroy()
    {
        this.destroyToken?.Cancel();
        this.destroyToken?.Dispose();

        this.Deactivate();
    }
    
    protected virtual void Deactivate()
    {
        this.deactiveToken?.Cancel();
        this.deactiveToken?.Dispose();
        this.deactiveToken = null;
    }

    
    CancellationTokenSource? deactiveToken;
    protected CancellationToken DeactiveToken
    {
        get
        {
            this.deactiveToken ??= new CancellationTokenSource();
            return this.deactiveToken.Token;
        }
    }
    
    
    CancellationTokenSource? destroyToken;
    protected CancellationToken DestroyToken
    {
        get
        {
            this.destroyToken ??= new CancellationTokenSource();
            return this.destroyToken.Token;
        }
    }
}