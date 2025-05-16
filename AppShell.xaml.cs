using Microsoft.Maui.Controls;

namespace Atestat4
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("signin", typeof(SignInPage));
            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("home", typeof(Views.NewPage1));
        }
    }
}
