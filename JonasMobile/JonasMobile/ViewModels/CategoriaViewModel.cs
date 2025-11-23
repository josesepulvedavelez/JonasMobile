using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JonasMobile.Models;
using JonasMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonasMobile.ViewModels
{
    public partial class CategoriaViewModel : ObservableObject
    {
        private readonly IAppService _appService;

        [ObservableProperty]
        public bool isLoading;

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

    }
}
