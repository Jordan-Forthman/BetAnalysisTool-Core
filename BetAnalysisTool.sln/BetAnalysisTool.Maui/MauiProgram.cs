using Microsoft.Extensions.Logging;
using BetAnalysisTool.Maui.ViewModels;
using BetAnalysisTool.Maui.Views;
using Auth0.OidcClient;

namespace BetAnalysisTool.Maui
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

            // --- START ADDED AUTH0 CODE ---
            builder.Services.AddSingleton(new Auth0Client(new()
            {
                Domain = "bet-analysis.us.auth0.com",
                ClientId = "gzCS6dcuQ6OwEIi6LLj9LjfJsyGC7YWj",
                RedirectUri = "myapp://callback",
                PostLogoutRedirectUri = "myapp://callback",
                Scope = "openid profile email"
            }));
            // --- END ADDED AUTH0 CODE ---

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginPageViewModel>();
            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<BackendTest>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
