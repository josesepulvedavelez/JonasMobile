using JonasMobile.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JonasMobile.Services
{
    public class AppService : IAppService
    {
        private readonly SQLiteAsyncConnection _database;

        public AppService()
        {
            string dbName = "Jonas.db3";
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, dbName);
                        
            if (!File.Exists(dbPath))
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(dbName).Result;
                using var dest = File.Create(dbPath);
                stream.CopyTo(dest);
            }

            System.Diagnostics.Debug.WriteLine($"DB PATH: {dbPath}");

            _database = new SQLiteAsyncConnection(dbPath);

            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            await _database.CreateTableAsync<Categoria>();
            await _database.CreateTableAsync<Animal>();
            await _database.CreateTableAsync<Media>();

            int countCategorias = await _database.Table<Categoria>().CountAsync();
            int countAnimales = await _database.Table<Animal>().CountAsync();
            int countMedias = await _database.Table<Media>().CountAsync();
        }

        public Task<List<Categoria>> GetAllCategoriasAsync()
        {
            return _database.Table<Categoria>().Where(x => x.Estado == 1).ToListAsync();
        }

        public async Task<List<Animal>> GetAllAnimalesByCategoriaAsync(int categoriaId)
        {
            var animales = await _database.Table<Animal>()
                .Where(a => a.Estado == 1)
                .Where(a => a.CategoriaId == categoriaId)
                .ToListAsync();

            var animalIds = animales.Select(a => a.AnimalId).ToList();

            var medias = await _database.Table<Media>()
                .Where(m => animalIds.Contains(m.AnimalId))
                .ToListAsync();
                       
            foreach (var animal in animales)
            {
                animal.Medias = medias
                    .Where(m => m.AnimalId == animal.AnimalId)
                    .ToList();
            }

            return animales;
        }

        public Task<List<Media>> GetAllMediasByAnimalAsync(int animalId)
        {
            return _database.Table<Media>()
                             .Where(m => m.AnimalId == animalId)
                             .ToListAsync();
        }

    }
}
