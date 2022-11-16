namespace TubePlayer.ViewControls.Common;

public partial class ErrorIndicator : VerticalStackLayout
{
	public ErrorIndicator()
	{
		InitializeComponent();
	}

	// Bindable Properties
	public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
		"IsError",
		typeof(bool),
		typeof(ErrorIndicator),
		false,
		BindingMode.OneWay,
		null,
		SetIsError);

	public bool IsError
	{
		get { return (bool)GetValue(IsErrorProperty); }
		set { SetValue(IsErrorProperty, value); }
	}

	private static void SetIsError(BindableObject bindable, object oldValue, object newValue) =>
		(bindable as ErrorIndicator).IsVisible = (bool)newValue;

    
    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        "ErrorText",
        typeof(string),
        typeof(ErrorIndicator),
        string.Empty,
        BindingMode.OneWay,
        null,
        SetErrorText);

    public string ErrorText
    {
        get { return (string)GetValue(ErrorTextProperty); }
        set { SetValue(ErrorTextProperty, value); }
    }

    private static void SetErrorText(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).lblErrorText.Text = (string)newValue;

    
    public static readonly BindableProperty ErrorImageProperty = BindableProperty.Create(
        "ErrorImage",
        typeof(ImageSource),
        typeof(ErrorIndicator),
        null,
        BindingMode.OneWay,
        null,
        SetErrorImage);

    public ImageSource ErrorImage
    {
        get { return (ImageSource)GetValue(ErrorImageProperty); }
        set { SetValue(ErrorImageProperty, value); }
    }

    private static void SetErrorImage(BindableObject bindable, object oldValue, object newValue) =>
        (bindable as ErrorIndicator).imgError.Source = (ImageSource)newValue;

}