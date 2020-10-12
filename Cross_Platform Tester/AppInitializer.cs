using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Cross_Platform_Tester
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp.Android.StartApp();
            }

            return ConfigureApp.iOS.StartApp();
        }
    }
}