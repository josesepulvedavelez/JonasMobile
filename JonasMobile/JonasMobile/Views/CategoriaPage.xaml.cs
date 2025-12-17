using JonasMobile.Models;
using JonasMobile.ViewModels;
using Microsoft.Maui.Media;

namespace JonasMobile.Views;

public partial class CategoriaPage : ContentPage
{
    private readonly CategoriaViewModel _vm;

    public CategoriaPage(CategoriaViewModel vm)
	{
        InitializeComponent();
        BindingContext = _vm = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadCategoriasAsync();
    }

    private async void OnCategoriaTapped(object sender, EventArgs e)
    {
        if (sender is Image image && image.BindingContext is Categoria categoria)
        {
            var animalViewModel = new AnimalViewModel(_vm.AppService)
            {
                CategoriaId = categoria.CategoriaId
            };

            var animalPage = new AnimalPage(animalViewModel);
            await Navigation.PushAsync(animalPage);
        }
    }

    private async void OnPlayAudioTapped(object sender, TappedEventArgs e)
    {
        if (sender is Image image && image.BindingContext is Categoria categoria)
        {
            var texto = $"{categoria.Nombre}. {categoria.Descripcion}";

            // Buscar locales disponibles
            var locales = await TextToSpeech.Default.GetLocalesAsync();

            // 1️⃣ Español Colombia
            var localeEsCO = locales.FirstOrDefault(l =>
                l.Language.Equals("es-CO", StringComparison.OrdinalIgnoreCase));

            // 2️⃣ Cualquier español
            var localeEs = localeEsCO ??
                           locales.FirstOrDefault(l =>
                               l.Language.StartsWith("es", StringComparison.OrdinalIgnoreCase));

            // 3️⃣ Fallback final
            var localeFinal = localeEs ?? locales.FirstOrDefault();

            await TextToSpeech.Default.SpeakAsync(
                texto,
                new SpeechOptions
                {
                    Locale = localeFinal,
                    Pitch = 0.95f,
                    Volume = 1.0f
                });
        }
    }

}