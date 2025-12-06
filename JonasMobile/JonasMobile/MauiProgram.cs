using JonasMobile.Services;
using JonasMobile.ViewModels;
using JonasMobile.Views;
using Microsoft.Extensions.Logging;

namespace JonasMobile
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

            builder.Services.AddSingleton<IAppService, AppService>();            
            builder.Services.AddTransient<CategoriaViewModel>();            
            builder.Services.AddTransient<CategoriaPage>();
            builder.Services.AddTransient<AnimalViewModel>();
            builder.Services.AddTransient<AnimalPage>();

            return builder.Build();
        }
    }
}
