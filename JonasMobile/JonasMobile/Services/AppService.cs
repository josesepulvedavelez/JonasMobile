using JonasMobile.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JonasMobile.Services
{
    public class AppService : IAppService
    {
        private readonly SQLiteAsyncConnection _database;

        public AppService()
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Jonas.db3");

            _database = new SQLiteAsyncConnection(dbPath);

            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            await _database.CreateTableAsync<Categoria>();

            int count = await _database.Table<Categoria>().CountAsync();

            if (count == 0)
            {
                await InsertarDatosInicialesAsync();
            }                
        }

        public Task<List<Categoria>> GetAllCategoriasAsync()
        {
            return _database.Table<Categoria>().ToListAsync();
        }

        private async Task InsertarDatosInicialesAsync()
        {
            var categoriasIniciales = new List<Categoria>
            {
                new Categoria 
                { 
                    Nombre = "Mamíferos", 
                    Descripcion = "Animales con glándulas mamarias y pelo.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/74/California_sea_lion_in_La_Jolla_%2870568%29.jpg/640px-California_sea_lion_in_La_Jolla_%2870568%29.jpg" 
                },

                new Categoria 
                { 
                    Nombre = "Aves", Descripcion = "Animales con plumas y pico.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/Wandering_Albatross-_east_of_the_Tasman_Peninsula.jpg/640px-Wandering_Albatross-_east_of_the_Tasman_Peninsula.jpg" 
                },

                new Categoria 
                { 
                    Nombre = "Reptiles", Descripcion = "Animales de sangre fría con escamas.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Green_Sea_Turtle_grazing_seagrass.jpg/640px-Green_Sea_Turtle_grazing_seagrass.jpg" 
                },

                new Categoria 
                { 
                    Nombre = "Anfibios", Descripcion = "Viven en agua y en tierra.",
                    Media = "https://s2.ppllstatics.com/canarias7/www/multimedia/201908/01/media/cortadas/8bf-1-800x445_5348995_20190801100446--1248x702.jpg" 
                },

                new Categoria 
                { 
                    Nombre = "Peces", Descripcion = "Animales acuáticos.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Head_and_Trunk_of_Caribbean_Reef_Shark_at_Tiger_Beach_Bahamas.jpg/640px-Head_and_Trunk_of_Caribbean_Reef_Shark_at_Tiger_Beach_Bahamas.jpg" 
                },

                new Categoria 
                { 
                    Nombre = "Moluscos", Descripcion = "Invertebrados blandos marinos, con concha.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Octopus2.jpg/640px-Octopus2.jpg" 
                },

                new Categoria 
                { 
                    Nombre = "Crustáceos", Descripcion = "Invertebrados con exoesqueleto y patas articuladas.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Goneplax_rhomboides_2.jpg/640px-Goneplax_rhomboides_2.jpg" 
                }
            };

            await _database.InsertAllAsync(categoriasIniciales);
        }

    }
}
