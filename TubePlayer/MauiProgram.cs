using Microsoft.Extensions.Logging;


namespace TubePlayer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCompatibility()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("FiraSans-Light.ttf", "RegularFont");
                    fonts.AddFont("FiraSans-Medium.ttf", "MediumFont");
                }).ConfigureMauiHandlers(handlers =>
                {
                    // Add legacy media element renderer
                    handlers.AddCompatibilityRenderer(
                        typeof(Xamarin.CommunityToolkit.UI.Views.MediaElement),
                        typeof(Xamarin.CommunityToolkit.UI.Views.MediaElementRenderer));
                });
//              .ConfigureLifecycleEvents(events =>
//                        {
//#if ANDROID
//            				events.AddAndroid(android => android.OnCreate((activity, bundle) => MakeStatusBarTranslucent(activity)));

//                            static void MakeStatusBarTranslucent(Android.App.Activity activity)
//                            {
//            					activity.Window.SetFlags(Android.Views.WindowManagerFlags.LayoutNoLimits, Android.Views.WindowManagerFlags.LayoutNoLimits);
//            					activity.Window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
//            					activity.Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
//                            }
//#endif
//                        });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Register services
            RegisterAppServices(builder.Services);

            return builder.Build();
        }

        private static void RegisterAppServices(IServiceCollection services)
        {
            // Add platform specific dependencies
            services.AddSingleton<IConnectivity>(Connectivity.Current);

            // Register Cache Barrel
            Barrel.ApplicationId = Constants.ApplicationId;
            services.AddSingleton<IBarrel>(Barrel.Current);

            // Regsiter API service
            services.AddSingleton<IApiService, YoutubeService>();

            // Register FileDownloadService
            services.AddSingleton<IDownloadFileService, FileDownloadService>();

            // Register ViewModels
            services.AddSingleton<StartPageViewModel>();
            services.AddTransient<VideoDetailsPageViewModel>();
        }
    }
}