using JonasMobile.Views;

namespace JonasMobile
{
    public partial class App : Application
    {
        public App(CategoriaPage categoriaPage)
        {
            InitializeComponent();

            MainPage = new NavigationPage(categoriaPage);
        }
    }
}
