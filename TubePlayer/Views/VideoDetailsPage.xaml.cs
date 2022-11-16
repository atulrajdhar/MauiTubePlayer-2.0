namespace TubePlayer.Views;

public partial class VideoDetailsPage : ViewBase<VideoDetailsPageViewModel>
{
	public VideoDetailsPage(object initParams) : base(initParams)
	{
		InitializeComponent();

		this.ViewModelInitialized += (s, e) =>
		{
			(this.BindingContext as VideoDetailsPageViewModel).DownloadCompleted += VideoDetailsPage_DownloadCompleted;
		};
	}

    protected override void OnDisappearing()
    {
        (this.BindingContext as VideoDetailsPageViewModel).DownloadCompleted -= VideoDetailsPage_DownloadCompleted;

        base.OnDisappearing();
    }

    private void VideoDetailsPage_DownloadCompleted(object sender, EventArgs e)
    {
        // Video information downloaded. Start showing items
        if ((this.BindingContext as VideoDetailsPageViewModel).IsErrorState)
            return;

        if (this.AnimationIsRunning("TransitionAnimation"))
            return;

        var parentAnimation = new Animation
        {
            // Poster image animation
            {
                0.0,
                0.7,
                new Animation(v => HeaderView.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Video title container animation
            {
                0.4,
                0.7,
                new Animation(v => VideoTitle.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Video icons animation
            {
                0.5,
                0.7,
                new Animation(v => VideoIcons.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Channel details animation
            {
                0.6,
                0.8,
                new Animation(v => ChannelDetails.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Similar videos animation
            {
                0.7,
                0.9,
                new Animation(v => SimilarVideos.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Tags animation
            {
                0.65,
                0.85,
                new Animation(v => TagsView.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Description view animation
            {
                0.8,
                1,
                new Animation(v => DescriptionView.Opacity = v, 0, 1, Easing.CubicIn)
            },

            // Comments button animation
            {
                0.8,
                1,
                new Animation(v => btnComments.Opacity = v, 0, 1, Easing.CubicIn)
            }
        };

        // Commit the animation
        parentAnimation.Commit(this, "TransitionAnimation", 16, Constants.ExtraLongDuration, null,
            (v, c) =>
            {
                // Action to perform on completion (if any)
            });
    }
}