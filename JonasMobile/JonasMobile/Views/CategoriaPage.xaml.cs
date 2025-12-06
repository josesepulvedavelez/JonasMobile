using JonasMobile.Models;
using JonasMobile.ViewModels;

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

}