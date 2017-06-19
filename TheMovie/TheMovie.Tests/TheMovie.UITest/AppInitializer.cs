using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TheMovie.UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../../../TheMovie.Android/bin/Release/com.jairzjunior.themovie.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

