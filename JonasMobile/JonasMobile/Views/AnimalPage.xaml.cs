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
}