using Microsoft.UI.Xaml;
using BetAnalysisTool.Maui;
//using Auth0.OidcClient.Platforms.Windows;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BetAnalysisTool.Maui.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            // 2. ADD THIS LINE (Must be before InitializeComponent)
            Auth0.OidcClient.Platforms.Windows.Activator.Default.CheckRedirectionActivation();

            this.InitializeComponent();
            //Microsoft.Maui.Controls.Compatibility.Forms.Init();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }

}
