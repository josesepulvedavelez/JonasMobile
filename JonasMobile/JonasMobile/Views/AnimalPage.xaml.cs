using JonasMobile.Models;
using JonasMobile.ViewModels;

namespace JonasMobile.Views;

public partial class AnimalPage : ContentPage
{
	public AnimalPage(AnimalViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((AnimalViewModel)BindingContext).LoadAnimalesAsync();
    }

    private async void OnPlayAnimalAudioTapped(object sender, TappedEventArgs e)
    {
        if (sender is Image image && image.BindingContext is Animal animal)
        {
            var texto =
                $"{animal.NombreComun}. " +
                $"{animal.NombreCientifico}. " +
                $"{animal.Descripcion}" +
                $"Habita en {animal.Habitat}. " +
                $"Se alimenta de {animal.Alimentacion}. " +
                $"Es un {animal.Tipo}. " +
                $"Mide alrrededor de {animal.Tamano}, " +
                $"Pesa alrrededor de {animal.PesoPromedio} kilogramos. " +
                $"Su estado de conservaciones {animal.EstadoConservacion}. ";

            var locales = await TextToSpeech.Default.GetLocalesAsync();

            var localeFinal =
                locales.FirstOrDefault(l => l.Language.Equals("es-CO", StringComparison.OrdinalIgnoreCase)) ??
                locales.FirstOrDefault(l => l.Language.StartsWith("es", StringComparison.OrdinalIgnoreCase)) ??
                locales.FirstOrDefault();

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