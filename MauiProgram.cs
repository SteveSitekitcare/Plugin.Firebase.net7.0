using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Analytics;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Core.Platforms.Android;

namespace MauiApp1
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif

            return builder
                .RegisterFirebaseServices()
                .Build();
        }

        private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder)
        {
            builder.ConfigureLifecycleEvents(events => {

                events.AddAndroid(android => android.OnCreate((activity, _) =>
                { 
                    CrossFirebase.Initialize(activity);
                    FirebaseAnalyticsImplementation.Initialize(activity);
                }));

            });

            builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);
            return builder;
        }
    }
}