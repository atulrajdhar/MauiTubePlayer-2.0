namespace TubePlayer.Models;
public static class Constants
{
    public static string ApplicationName = "Tube Player";
    public static string EmailAddress = @"tubeplayer@asrtech.com";
    public static string ApplicationId = "ASRTECH.TubePlayer.App";
    public static string ApiServiceURL = @"https://youtube.googleapis.com/youtube/v3/";
    public static string ApiKey = @"AIzaSyD0f8Pl2RznEx0Lnj60Pg41HhqaQcBNucc";

    public static uint MicroDuration { get; set; } = 100;
    public static uint SmallDuration { get; set; } = 300;
    public static uint MediumDuration { get; set; } = 600;
    public static uint LongDuration { get; set; } = 1200;
    public static uint ExtraLongDuration { get; set; } = 1800;
}
