using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JonasMobile.Models;
using JonasMobile.Services;
using JonasMobile.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace JonasMobile.ViewModels
{
    public partial class CategoriaViewModel : ObservableObject
    {
        private readonly IAppService _appService;
        public IAppService AppService => _appService;

        [ObservableProperty]
        private bool isLoading;

        public ObservableCollection<Categoria> Categorias { get; } = new ObservableCollection<Categoria>();

        public CategoriaViewModel(IAppService appService)
        {
            _appService = appService;
        }

        [RelayCommand]
        public async Task LoadCategoriasAsync()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                Categorias.Clear();
                var items = await _appService.GetAllCategoriasAsync();
                foreach (var item in items)
                    Categorias.Add(item);                
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task CategoriaSelected(Categoria categoria)
        {
            var animalViewModel = new AnimalViewModel(_appService)
            {
                CategoriaId = categoria.CategoriaId
            };

            var animalPage = new AnimalPage(animalViewModel);
            await Shell.Current.Navigation.PushAsync(animalPage);
        }

    }
}
