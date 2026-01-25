using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetAnalysisTool.Maui.ViewModels;

namespace BetAnalysisTool.Maui.Views
{
    public partial class LoginPage : ContentPage
    {
        // We ask for the ViewModel here (Dependency Injection)
        public LoginPage(LoginPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm; // This connects the View to the ViewModel!
        }
    }
}