namespace Maui.Apps.Framework.MVVM;
public partial class ViewModelBase : ObservableObject
{
    [ObservableProperty]
    private string title = string.Empty;

    [ObservableProperty]
    private bool isBusy = false;

    [ObservableProperty]
    private string loadingText = string.Empty;

    [ObservableProperty]
    private bool dataLoaded = false;

    [ObservableProperty]
    private bool isErrorState = false;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string errorImage = string.Empty;

    public ViewModelBase() =>
        IsErrorState = false;

    // Called on page appearing
    public virtual async void OnNavigatedTo(object parameters) =>
        await Task.CompletedTask;

    // Set loading indicators for page
    protected void SetDataLoadingIndicators(bool isStaring = true)
    {
        if (isStaring)
        {
            IsBusy = true;
            DataLoaded = false;
            IsErrorState = false;
            ErrorMessage = "";
            ErrorImage = "";
        }
        else
        {
            LoadingText = "";
            IsBusy = false;
        }
    }
}
