namespace TubePlayer.IServices;
public interface IApiService
{
    Task<VideoSearchResult> SearchVideos(string searchQuery, string nextPageToken = "");
    Task<ChannelSearchResult> GetChannels(string channelIDs);
    Task<YoutubeVideoDetail> GetVideoDetails(string videoID);
    Task<CommentSearchResult> GetComments(string videoID);
}
