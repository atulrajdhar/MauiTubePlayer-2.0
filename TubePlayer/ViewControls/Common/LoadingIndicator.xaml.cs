namespace TubePlayer.ViewControls.Common;

public partial class LoadingIndicator : VerticalStackLayout
{
	// Bindable Properties
	public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
		"IsBusy",
		typeof(bool),
		typeof(LoadingIndicator),
		false,
		BindingMode.OneWay,
		null,
		SetIsBusy);

	public bool IsBusy
	{
		get { return (bool)GetValue(IsBusyProperty); }
		set { SetValue(IsBusyProperty, value); }
	}

    private static void SetIsBusy(BindableObject bindable, object oldValue, object newValue)
    {
        LoadingIndicator control = bindable as LoadingIndicator;

		control.IsVisible = (bool)newValue;
		control.actIndicator.IsRunning = (bool)newValue;
    }

    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(
        "LoadingText",
        typeof(string),
        typeof(LoadingIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetLoadingText);

    public bool LoadingText
    {
        get { return (bool)GetValue(LoadingTextProperty); }
        set { SetValue(LoadingTextProperty, value); }
    }

    private static void SetLoadingText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as LoadingIndicator).lblLoadingText.Text = (string)newValue;

    public LoadingIndicator()
	{
		InitializeComponent();
	}
}