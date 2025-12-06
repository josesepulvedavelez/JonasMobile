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
    public partial class AnimalViewModel : ObservableObject
    {
        private readonly IAppService _appService;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private int categoriaId;

        public ObservableCollection<Animal> Animales { get; } = new ObservableCollection<Animal>();

        public AnimalViewModel(IAppService appService)
        {
            _appService = appService;
        }

        [RelayCommand]
        public async Task LoadAnimalesAsync()
        {
            if (IsLoading) return;

            try
            {
                IsLoading = true;
                Animales.Clear();
                                
                var animales = await _appService.GetAllAnimalesByCategoriaAsync(CategoriaId);

                foreach (var animal in animales)                
                    Animales.Add(animal);
                
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
