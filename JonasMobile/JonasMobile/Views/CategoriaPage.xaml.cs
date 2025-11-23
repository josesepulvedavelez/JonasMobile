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
}