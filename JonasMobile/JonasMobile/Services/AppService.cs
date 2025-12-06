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
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Jonas.db3");
            //File.Delete(dbPath);
            _database = new SQLiteAsyncConnection(dbPath);

            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            //await _database.DropTableAsync<Categoria>();
            //await _database.DropTableAsync<Animal>();

            await _database.CreateTableAsync<Categoria>();
            await _database.CreateTableAsync<Animal>();

            int countCategorias = await _database.Table<Categoria>().CountAsync();
            int countAnimales = await _database.Table<Animal>().CountAsync();

            if (countCategorias == 0)
                await InsertCategoriasAsync();

            if (countAnimales == 0)
                await InsertAnimalesAsync();

            var categorias = await _database.Table<Categoria>().CountAsync();
            var animales = await _database.Table<Animal>().CountAsync();
        }

        public Task<List<Categoria>> GetAllCategoriasAsync()
        {
            return _database.Table<Categoria>().ToListAsync();
        }

        public Task<List<Animal>> GetAllAnimalesByCategoriaAsync(int categoriaId)
        {
            return _database.Table<Animal>()
                             .Where(a => a.CategoriaId == categoriaId)
                             .ToListAsync();
        }

        private async Task InsertCategoriasAsync()
        {
            var categoriasIniciales = new List<Categoria>
            {
                new Categoria
                {
                    CategoriaId = 1,
                    Nombre = "Mamíferos",
                    Descripcion = "Animales con glándulas mamarias y pelo.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/7/74/California_sea_lion_in_La_Jolla_%2870568%29.jpg/640px-California_sea_lion_in_La_Jolla_%2870568%29.jpg"
                },
                new Categoria
                {
                    CategoriaId = 2,
                    Nombre = "Aves",
                    Descripcion = "Animales con plumas y pico.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/ac/Wandering_Albatross-_east_of_the_Tasman_Peninsula.jpg/640px-Wandering_Albatross-_east_of_the_Tasman_Peninsula.jpg"
                },
                new Categoria
                {
                    CategoriaId = 3,
                    Nombre = "Reptiles",
                    Descripcion = "Animales de sangre fría con escamas.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Green_Sea_Turtle_grazing_seagrass.jpg/640px-Green_Sea_Turtle_grazing_seagrass.jpg"
                },
                new Categoria
                {
                    CategoriaId = 4,
                    Nombre = "Anfibios",
                    Descripcion = "Viven en agua y en tierra.",
                    Media = "https://s2.ppllstatics.com/canarias7/www/multimedia/201908/01/media/cortadas/8bf-1-800x445_5348995_20190801100446--1248x702.jpg"
                },
                new Categoria
                {
                    CategoriaId = 5,
                    Nombre = "Peces",
                    Descripcion = "Animales acuáticos.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/6/6d/Head_and_Trunk_of_Caribbean_Reef_Shark_at_Tiger_Beach_Bahamas.jpg/640px-Head_and_Trunk_of_Caribbean_Reef_Shark_at_Tiger_Beach_Bahamas.jpg"
                },
                new Categoria
                {
                    CategoriaId = 6,
                    Nombre = "Moluscos",
                    Descripcion = "Invertebrados marinos, con concha o cuerpo blando.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/57/Octopus2.jpg/640px-Octopus2.jpg"
                },
                new Categoria
                {
                    CategoriaId = 7,
                    Nombre = "Crustáceos",
                    Descripcion = "Invertebrados marinos con exoesqueleto y patas articuladas.",
                    Media = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/21/Goneplax_rhomboides_2.jpg/640px-Goneplax_rhomboides_2.jpg"
                }
            };
            
            await _database.InsertAllAsync(categoriasIniciales);
        }

        public async Task InsertAnimalesAsync()
        {
            var animalesIniciales = new List<Animal>
            {
                new Animal
                {
                    AnimalId = 1,
                    NombreComun = "Delfín",
                    NombreCientifico = "Delphinus delphis",
                    Descripcion = "Mamífero acuático inteligente y sociable.",
                    Habitat = "Océanos y mares de todo el mundo",
                    Alimentacion = "Carnívoro (peces y calamares)",
                    Tipo = "Mamífero",
                    Tamano = "2 - 4 metros",
                    PesoPromedio = 150,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 2,
                    NombreComun = "Ballena Azul",
                    NombreCientifico = "Balaenoptera musculus",
                    Descripcion = "El animal más grande del mundo.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Carnívoro (krill)",
                    Tipo = "Mamífero",
                    Tamano = "24 - 30 metros",
                    PesoPromedio = 170000,
                    EstadoConservacion = "En peligro",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 3,
                    NombreComun = "Foca",
                    NombreCientifico = "Phocidae",
                    Descripcion = "Mamífero adaptado a la vida acuática y terrestre.",
                    Habitat = "Costas y mares fríos",
                    Alimentacion = "Carnívoro (peces y crustáceos)",
                    Tipo = "Mamífero",
                    Tamano = "1.5 - 2 metros",
                    PesoPromedio = 100,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 4,
                    NombreComun = "Manatí",
                    NombreCientifico = "Trichechus",
                    Descripcion = "Mamífero acuático herbívoro, de movimientos lentos.",
                    Habitat = "Ríos y costas tropicales",
                    Alimentacion = "Herbívoro (plantas acuáticas)",
                    Tipo = "Mamífero",
                    Tamano = "2.5 - 4 metros",
                    PesoPromedio = 500,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 5,
                    NombreComun = "Narval",
                    NombreCientifico = "Monodon monoceros",
                    Descripcion = "Mamífero marino con un colmillo largo en forma de espiral.",
                    Habitat = "Océano Ártico",
                    Alimentacion = "Carnívoro (peces y calamares)",
                    Tipo = "Mamífero",
                    Tamano = "4 - 5.5 metros",
                    PesoPromedio = 1500,
                    EstadoConservacion = "Casi amenazado",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 6,
                    NombreComun = "Ornitorrinco",
                    NombreCientifico = "Ornithorhynchus anatinus",
                    Descripcion = "Mamífero ovíparo con pico de pato y cola de castor.",
                    Habitat = "Ríos y lagos de Australia",
                    Alimentacion = "Carnívoro (insectos y crustáceos)",
                    Tipo = "Mamífero",
                    Tamano = "30 - 45 cm",
                    PesoPromedio = 1.5m,
                    EstadoConservacion = "Casi amenazado",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 7,
                    NombreComun = "León Marino",
                    NombreCientifico = "Otaria flavescens",
                    Descripcion = "Mamífero marino con aletas y gran agilidad en el agua.",
                    Habitat = "Costas de América del Sur",
                    Alimentacion = "Carnívoro (peces y moluscos)",
                    Tipo = "Mamífero",
                    Tamano = "2 - 2.5 metros",
                    PesoPromedio = 300,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 8,
                    NombreComun = "Ballena Jorobada",
                    NombreCientifico = "Megaptera novaeangliae",
                    Descripcion = "Ballena conocida por sus saltos acrobáticos.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Carnívoro (krill y peces pequeños)",
                    Tipo = "Mamífero",
                    Tamano = "12 - 16 metros",
                    PesoPromedio = 30000,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 9,
                    NombreComun = "Morsa",
                    NombreCientifico = "Odobenus rosmarus",
                    Descripcion = "Mamífero marino con colmillos largos y cuerpo robusto.",
                    Habitat = "Regiones árticas",
                    Alimentacion = "Carnívoro (moluscos y peces)",
                    Tipo = "Mamífero",
                    Tamano = "2.5 - 3.5 metros",
                    PesoPromedio = 1000,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 1
                },
                new Animal
                {
                    AnimalId = 10,
                    NombreComun = "Dugongo",
                    NombreCientifico = "Dugong dugon",
                    Descripcion = "Mamífero marino herbívoro, similar al manatí.",
                    Habitat = "Costas del océano Índico y Pacífico",
                    Alimentacion = "Herbívoro (hierbas marinas)",
                    Tipo = "Mamífero",
                    Tamano = "2.5 - 3 metros",
                    PesoPromedio = 300,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 1
                }
            };
            
            await _database.InsertAllAsync(animalesIniciales);
        }

    }
}
