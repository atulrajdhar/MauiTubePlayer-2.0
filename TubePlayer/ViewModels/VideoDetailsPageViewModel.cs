﻿using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace TubePlayer.ViewModels;
public partial class VideoDetailsPageViewModel : AppViewModelBase
{
    [ObservableProperty]
    private YoutubeVideoDetail theVideo;

    [ObservableProperty]
    private List<YoutubeVideo> similarVideos;

    [ObservableProperty]
    private Channel theChannel;    

    [ObservableProperty]
    private List<Comment> comments;

    public event EventHandler DownloadCompleted;

    [ObservableProperty]
    private string videoSource;

    [ObservableProperty]
    private double progressValue;

    [ObservableProperty]
    private bool isDownloading = false;

    private IEnumerable<MuxedStreamInfo> streamInfo;

    private IDownloadFileService _fileDownloadService;

    public VideoDetailsPageViewModel(IApiService appApiService, IDownloadFileService fileDownloadService) : base(appApiService)
    {
        this._fileDownloadService = fileDownloadService;
        this.Title = "TUBE PLAYER";
    }

    public async override void OnNavigatedTo(object parameters)
    {
        var videoID = (string)parameters;

        SetDataLoadingIndicators(true);

        this.LoadingText = "Hold on while we load the video details...";

        try
        {
            SimilarVideos = new();
            Comments = new();

            // Get Video Details
            TheVideo = await _appApiService.GetVideoDetails(videoID);

            // Get channel URL and set in the video
            var channelSearchResult = await _appApiService.GetChannels(TheVideo.Snippet.ChannelId);
            TheChannel = channelSearchResult.Items.First();

            // Find similar videos
            if (TheVideo.Snippet.Tags is not null)
            {
                var similarVideosSearchResult = await _appApiService.SearchVideos(TheVideo.Snippet.Tags.First(), "");

                SimilarVideos = similarVideosSearchResult.Items;                
            }

            // Get Comments
            var commentsSearchResult = await _appApiService.GetComments(videoID);
            Comments = commentsSearchResult.Items;

            // Get stream URL
            await GetVideoURL();

            // Raise data load completed event to the UI
            this.DataLoaded = true;

            // Raise the event to notify the UI of download completion
            DownloadCompleted?.Invoke(this, new EventArgs());

        }
        catch (InternetConnectionException iex)
        {
            this.IsErrorState = true;
            this.ErrorMessage = "Slow or no internet connection." + Environment.NewLine + "Please check your internet connection and try again. ";
            ErrorImage = "nointernet.png";
        }
        catch (Exception ex)
        {
            this.IsErrorState = true;
            this.ErrorMessage = $"Something went wrong. If the problem persists, please contact support at {Constants.EmailAddress} with the error message:" + Environment.NewLine + Environment.NewLine + ex.Message;
            ErrorImage = "error.png";
        }
        finally
        {
            SetDataLoadingIndicators(false);
        }
    }    

    [RelayCommand]
    private async Task UnlikeVideo()
    {
        await PageService.DisplayAlert("Comming Soon",
            "The unlike option is comming soon, once we implement the OAuth login functionality.",
            "OK");
    }

    [RelayCommand]
    private async Task ShareVideo()
    {
        var textToShare = $"Hey, I found this amazing video. Check it out: https://www.youtube.com/watch?v={TheVideo.Id}";

        // Share
        await Share.RequestAsync(new ShareTextRequest
        {
            Text = textToShare,
            Title = TheVideo.Snippet.Title
        });
    }

    [RelayCommand]
    private async Task DownloadVideo()
    {
        if (IsDownloading)
            return;

        var progressIndicator = new Progress<double>((value) => ProgressValue = value);
        var cts = new CancellationTokenSource();

        try
        {
            IsDownloading = true;

            var urlToDownload = streamInfo.OrderByDescending(video => video.VideoResolution.Area).First().Url;

            // Download the file
            var downloadedFilePath = await _fileDownloadService.DownloadFileAsync(
                urlToDownload,
                TheVideo.Snippet.Title.CleanCacheKey() + ".mp4",
                progressIndicator,
                cts.Token);

            // Save the file
            await Share.RequestAsync(new ShareFileRequest
            {
                File = new ShareFile(downloadedFilePath),
                Title = TheVideo.Snippet.Title
            });
        }
        catch(OperationCanceledException ex)
        {
            // Handle exception and cancellation token here
        }
        finally
        {
            IsDownloading = false;
        }
    }

    [RelayCommand]
    private async Task SubscribeChannel()
    {
        await PageService.DisplayAlert("Comming Soon",
            "The subscribe to channel option is comming soon, once we implement the OAuth login functionality.",
            "OK");
    }

    [RelayCommand]
    private async Task NavigateToVideoDetailsPage(string videoID)
    {
        await NavigationService.PushAsync(new VideoDetailsPage(videoID));
    }

    private async Task GetVideoURL()
    {
        var youtube = new YoutubeClient();

        var streamManifest = await youtube.Videos.Streams.GetManifestAsync(
            $"https://youtube.com/watch?v={TheVideo.Id}"
        );

        // Get highest quality muxed stream
        streamInfo = streamManifest.GetMuxedStreams();

        var videoPlayerStream = streamInfo.First(video => video.VideoQuality.Label is "240p" or "360p" or "480p");

        VideoSource = videoPlayerStream.Url;
    }
}
