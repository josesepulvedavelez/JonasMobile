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

            //await _database.DeleteAllAsync<Categoria>();
            //await _database.DeleteAllAsync<Animal>();

            await _database.CreateTableAsync<Categoria>();
            await _database.CreateTableAsync<Animal>();

            int countCategorias = await _database.Table<Categoria>().CountAsync();
            int countAnimales = await _database.Table<Animal>().CountAsync();

            if (countCategorias == 0)
                await InsertCategoriasAsync();

            if (countAnimales == 0)
                await InsertAnimalesAsync();

            //var categorias = await _database.Table<Categoria>().CountAsync();
            //var animales = await _database.Table<Animal>().CountAsync();
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
                },

                new Animal
                {
                    AnimalId = 11,
                    NombreComun = "Albatros",
                    NombreCientifico = "Diomedea exulans",
                    Descripcion = "Ave marina de gran envergadura.",
                    Habitat = "Océanos del hemisferio sur",
                    Alimentacion = "Carnívoro (peces y calamares)",
                    Tipo = "Ave",
                    Tamano = "1.1 - 1.3 metros",
                    PesoPromedio = 8,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 12,
                    NombreComun = "Pingüino Emperador",
                    NombreCientifico = "Aptenodytes forsteri",
                    Descripcion = "Ave no voladora adaptada al frío extremo.",
                    Habitat = "Antártida",
                    Alimentacion = "Carnívoro (peces y krill)",
                    Tipo = "Ave",
                    Tamano = "1.1 - 1.3 metros",
                    PesoPromedio = 30,
                    EstadoConservacion = "Casi amenazado",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 13,
                    NombreComun = "Pelícano",
                    NombreCientifico = "Pelecanus occidentalis",
                    Descripcion = "Ave marina con pico largo y bolsa gular.",
                    Habitat = "Costas de América",
                    Alimentacion = "Carnívoro (peces)",
                    Tipo = "Ave",
                    Tamano = "1.3 - 1.8 metros",
                    PesoPromedio = 5,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 14,
                    NombreComun = "Gaviota",
                    NombreCientifico = "Larus argentatus",
                    Descripcion = "Ave marina adaptada a la vida costera.",
                    Habitat = "Costas de todo el mundo",
                    Alimentacion = "Omnívoro (peces, crustáceos, desechos)",
                    Tipo = "Ave",
                    Tamano = "50 - 60 cm",
                    PesoPromedio = 1,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 15,
                    NombreComun = "Cormorán",
                    NombreCientifico = "Phalacrocorax carbo",
                    Descripcion = "Ave marina que bucea para atrapar peces.",
                    Habitat = "Costas y ríos de todo el mundo",
                    Alimentacion = "Carnívoro (peces)",
                    Tipo = "Ave",
                    Tamano = "80 - 100 cm",
                    PesoPromedio = 2.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 16,
                    NombreComun = "Fragata",
                    NombreCientifico = "Fregata magnificens",
                    Descripcion = "Ave marina con gran envergadura y vuelo elegante.",
                    Habitat = "Océanos tropicales",
                    Alimentacion = "Carnívoro (peces y calamares)",
                    Tipo = "Ave",
                    Tamano = "85 - 115 cm",
                    PesoPromedio = 1.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 17,
                    NombreComun = "Pato Marino",
                    NombreCientifico = "Melanitta perspicillata",
                    Descripcion = "Ave acuática adaptada a la vida en el mar.",
                    Habitat = "Costas de América del Norte",
                    Alimentacion = "Omnívoro (moluscos y plantas)",
                    Tipo = "Ave",
                    Tamano = "45 - 55 cm",
                    PesoPromedio = 1.2m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 18,
                    NombreComun = "Charran",
                    NombreCientifico = "Sterna hirundo",
                    Descripcion = "Ave marina migratoria de vuelo ágil.",
                    Habitat = "Costas de Europa, Asia y América",
                    Alimentacion = "Carnívoro (peces pequeños)",
                    Tipo = "Ave",
                    Tamano = "30 - 40 cm",
                    PesoPromedio = 0.1m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 19,
                    NombreComun = "Piquero",
                    NombreCientifico = "Sula nebouxii",
                    Descripcion = "Ave marina que se zambulle para pescar.",
                    Habitat = "Islas del Pacífico",
                    Alimentacion = "Carnívoro (peces)",
                    Tipo = "Ave",
                    Tamano = "75 - 90 cm",
                    PesoPromedio = 1.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 2
                },
                new Animal
                {
                    AnimalId = 20,
                    NombreComun = "Aguilucho Lagunero",
                    NombreCientifico = "Circus aeruginosus",
                    Descripcion = "Ave rapaz que habita en humedales y costas.",
                    Habitat = "Europa, Asia y África",
                    Alimentacion = "Carnívoro (aves y pequeños mamíferos)",
                    Tipo = "Ave",
                    Tamano = "45 - 55 cm",
                    PesoPromedio = 0.6m,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 2
                },

                new Animal
                {
                    AnimalId = 21,
                    NombreComun = "Tortuga Marina",
                    NombreCientifico = "Chelonioidea",
                    Descripcion = "Reptil marino con caparazón.",
                    Habitat = "Océanos tropicales",
                    Alimentacion = "Omnívoro",
                    Tipo = "Reptil",
                    Tamano = "0.7 - 2 metros",
                    PesoPromedio = 130,
                    EstadoConservacion = "En peligro",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 22,
                    NombreComun = "Cocodrilo Marino",
                    NombreCientifico = "Crocodylus porosus",
                    Descripcion = "Reptil acuático depredador.",
                    Habitat = "Ríos y costas del sudeste asiático y Australia",
                    Alimentacion = "Carnívoro",
                    Tipo = "Reptil",
                    Tamano = "4 - 7 metros",
                    PesoPromedio = 1000,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 23,
                    NombreComun = "Iguana Marina",
                    NombreCientifico = "Amblyrhynchus cristatus",
                    Descripcion = "Reptil adaptado a la vida en el mar, único en las Galápagos.",
                    Habitat = "Islas Galápagos",
                    Alimentacion = "Herbívoro (algas)",
                    Tipo = "Reptil",
                    Tamano = "0.5 - 1.2 metros",
                    PesoPromedio = 1.5m,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 24,
                    NombreComun = "Tortuga Carey",
                    NombreCientifico = "Eretmochelys imbricata",
                    Descripcion = "Tortuga marina con caparazón en forma de pico de halcón.",
                    Habitat = "Océanos tropicales",
                    Alimentacion = "Omnívoro (esponjas y medusas)",
                    Tipo = "Reptil",
                    Tamano = "0.6 - 1 metro",
                    PesoPromedio = 50,
                    EstadoConservacion = "En peligro crítico",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 25,
                    NombreComun = "Serpiente Marina",
                    NombreCientifico = "Hydrophiinae",
                    Descripcion = "Serpiente adaptada a la vida en el mar.",
                    Habitat = "Océanos Índico y Pacífico",
                    Alimentacion = "Carnívoro (peces)",
                    Tipo = "Reptil",
                    Tamano = "0.8 - 1.5 metros",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 26,
                    NombreComun = "Cocodrilo Americano",
                    NombreCientifico = "Crocodylus acutus",
                    Descripcion = "Reptil acuático de gran tamaño.",
                    Habitat = "América Central y del Sur",
                    Alimentacion = "Carnívoro",
                    Tipo = "Reptil",
                    Tamano = "3 - 5 metros",
                    PesoPromedio = 300,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 27,
                    NombreComun = "Tortuga Laúd",
                    NombreCientifico = "Dermochelys coriacea",
                    Descripcion = "Tortuga marina de gran tamaño y caparazón flexible.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Carnívoro (medusas)",
                    Tipo = "Reptil",
                    Tamano = "1.5 - 2.5 metros",
                    PesoPromedio = 500,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 28,
                    NombreComun = "Caimán",
                    NombreCientifico = "Caiman crocodilus",
                    Descripcion = "Reptil acuático de menor tamaño que el cocodrilo.",
                    Habitat = "América Central y del Sur",
                    Alimentacion = "Carnívoro",
                    Tipo = "Reptil",
                    Tamano = "1.5 - 2.5 metros",
                    PesoPromedio = 60,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 29,
                    NombreComun = "Tortuga de Río",
                    NombreCientifico = "Podocnemis expansa",
                    Descripcion = "Tortuga de agua dulce de gran tamaño.",
                    Habitat = "Ríos de América del Sur",
                    Alimentacion = "Omnívoro",
                    Tipo = "Reptil",
                    Tamano = "0.5 - 1 metro",
                    PesoPromedio = 10,
                    EstadoConservacion = "En peligro",
                    CategoriaId = 3
                },
                new Animal
                {
                    AnimalId = 30,
                    NombreComun = "Víbora de Gabón",
                    NombreCientifico = "Bitis gabonica",
                    Descripcion = "Serpiente venenosa con camuflaje en el suelo.",
                    Habitat = "África Central",
                    Alimentacion = "Carnívoro",
                    Tipo = "Reptil",
                    Tamano = "1 - 1.5 metros",
                    PesoPromedio = 5,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 3
                },

                new Animal
                {
                    AnimalId = 31,
                    NombreComun = "Rana Verde",
                    NombreCientifico = "Pelophylax perezi",
                    Descripcion = "Anfibio común en zonas húmedas.",
                    Habitat = "Europa",
                    Alimentacion = "Insectívoro",
                    Tipo = "Anfibio",
                    Tamano = "5 - 10 cm",
                    PesoPromedio = 0.02m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 32,
                    NombreComun = "Salamandra",
                    NombreCientifico = "Salamandra salamandra",
                    Descripcion = "Anfibio con cuerpo alargado y cola.",
                    Habitat = "Europa",
                    Alimentacion = "Carnívoro",
                    Tipo = "Anfibio",
                    Tamano = "10 - 20 cm",
                    PesoPromedio = 0.04m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 33,
                    NombreComun = "Tritón",
                    NombreCientifico = "Triturus",
                    Descripcion = "Anfibio similar a la salamandra, acuático.",
                    Habitat = "Europa",
                    Alimentacion = "Carnívoro",
                    Tipo = "Anfibio",
                    Tamano = "7 - 12 cm",
                    PesoPromedio = 0.01m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 34,
                    NombreComun = "Rana Flecha Azul",
                    NombreCientifico = "Dendrobates tinctorius",
                    Descripcion = "Rana venenosa de colores brillantes.",
                    Habitat = "América del Sur",
                    Alimentacion = "Insectívoro",
                    Tipo = "Anfibio",
                    Tamano = "4 - 6 cm",
                    PesoPromedio = 0.005m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 35,
                    NombreComun = "Sapo Común",
                    NombreCientifico = "Bufo bufo",
                    Descripcion = "Anfibio con piel verrugosa.",
                    Habitat = "Europa, Asia y África",
                    Alimentacion = "Insectívoro",
                    Tipo = "Anfibio",
                    Tamano = "8 - 15 cm",
                    PesoPromedio = 0.08m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 36,
                    NombreComun = "Axolote",
                    NombreCientifico = "Ambystoma mexicanum",
                    Descripcion = "Anfibio neoténico, permanece en estado larvario.",
                    Habitat = "México",
                    Alimentacion = "Carnívoro",
                    Tipo = "Anfibio",
                    Tamano = "15 - 30 cm",
                    PesoPromedio = 0.1m,
                    EstadoConservacion = "En peligro crítico",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 37,
                    NombreComun = "Rana Toro",
                    NombreCientifico = "Lithobates catesbeianus",
                    Descripcion = "Rana grande, conocida por su croar fuerte.",
                    Habitat = "América del Norte",
                    Alimentacion = "Carnívoro",
                    Tipo = "Anfibio",
                    Tamano = "10 - 20 cm",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 38,
                    NombreComun = "Cecilia",
                    NombreCientifico = "Gymnophiona",
                    Descripcion = "Anfibio sin patas, similar a una lombriz.",
                    Habitat = "América del Sur, África y Asia",
                    Alimentacion = "Carnívoro",
                    Tipo = "Anfibio",
                    Tamano = "30 - 100 cm",
                    PesoPromedio = 0.2m,
                    EstadoConservacion = "Datos insuficientes",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 39,
                    NombreComun = "Rana de Cristal",
                    NombreCientifico = "Cochranella granulosa",
                    Descripcion = "Rana con piel translúcida.",
                    Habitat = "América Central y del Sur",
                    Alimentacion = "Insectívoro",
                    Tipo = "Anfibio",
                    Tamano = "2 - 3 cm",
                    PesoPromedio = 0.002m,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 4
                },
                new Animal
                {
                    AnimalId = 40,
                    NombreComun = "Salamandra Gigante de China",
                    NombreCientifico = "Andrias davidianus",
                    Descripcion = "Anfibio acuático de gran tamaño.",
                    Habitat = "China",
                    Alimentacion = "Carnívoro",
                    Tipo = "Anfibio",
                    Tamano = "1 - 1.8 metros",
                    PesoPromedio = 25,
                    EstadoConservacion = "En peligro crítico",
                    CategoriaId = 4
                },

                new Animal
                {
                    AnimalId = 41,
                    NombreComun = "Tiburón Blanco",
                    NombreCientifico = "Carcharodon carcharias",
                    Descripcion = "Depredador marino de gran tamaño.",
                    Habitat = "Océanos templados",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "4 - 6 metros",
                    PesoPromedio = 1000,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 42,
                    NombreComun = "Pez Payaso",
                    NombreCientifico = "Amphiprioninae",
                    Descripcion = "Pez colorido, asociado a anémonas.",
                    Habitat = "Arrecifes de coral del Indo-Pacífico",
                    Alimentacion = "Omnívoro",
                    Tipo = "Pez",
                    Tamano = "6 - 10 cm",
                    PesoPromedio = 0.02m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 43,
                    NombreComun = "Atún",
                    NombreCientifico = "Thunnus",
                    Descripcion = "Pez migratorio de gran velocidad.",
                    Habitat = "Océanos templados y tropicales",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "1 - 3 metros",
                    PesoPromedio = 50,
                    EstadoConservacion = "En peligro",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 44,
                    NombreComun = "Salmón",
                    NombreCientifico = "Salmo salar",
                    Descripcion = "Pez migratorio, muy valorado en gastronomía.",
                    Habitat = "Ríos y océanos del hemisferio norte",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "70 - 150 cm",
                    PesoPromedio = 4,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 45,
                    NombreComun = "Caballito de Mar",
                    NombreCientifico = "Hippocampus",
                    Descripcion = "Pez con forma de caballo, macho gestante.",
                    Habitat = "Arrecifes de coral y praderas marinas",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "1.5 - 35 cm",
                    PesoPromedio = 0.01m,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 46,
                    NombreComun = "Pez Globo",
                    NombreCientifico = "Tetraodontidae",
                    Descripcion = "Pez que infla su cuerpo como defensa.",
                    Habitat = "Océanos tropicales",
                    Alimentacion = "Omnívoro",
                    Tipo = "Pez",
                    Tamano = "5 - 50 cm",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 47,
                    NombreComun = "Mero",
                    NombreCientifico = "Epinephelus",
                    Descripcion = "Pez de gran tamaño, habitante de arrecifes.",
                    Habitat = "Arrecifes de coral",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "50 - 250 cm",
                    PesoPromedio = 25,
                    EstadoConservacion = "En peligro",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 48,
                    NombreComun = "Pez Espada",
                    NombreCientifico = "Xiphias gladius",
                    Descripcion = "Pez con un pico alargado en forma de espada.",
                    Habitat = "Océanos templados y tropicales",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "3 - 4.5 metros",
                    PesoPromedio = 200,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 49,
                    NombreComun = "Anguila",
                    NombreCientifico = "Anguilla anguilla",
                    Descripcion = "Pez alargado, migratorio.",
                    Habitat = "Ríos y océanos de Europa",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "40 - 150 cm",
                    PesoPromedio = 1,
                    EstadoConservacion = "En peligro crítico",
                    CategoriaId = 5
                },
                new Animal
                {
                    AnimalId = 50,
                    NombreComun = "Pez León",
                    NombreCientifico = "Pterois",
                    Descripcion = "Pez venenoso con aletas en forma de abanico.",
                    Habitat = "Arrecifes de coral del Indo-Pacífico",
                    Alimentacion = "Carnívoro",
                    Tipo = "Pez",
                    Tamano = "20 - 40 cm",
                    PesoPromedio = 0.3m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 5
                },

                new Animal
                {
                    AnimalId = 51,
                    NombreComun = "Pulpo",
                    NombreCientifico = "Octopus vulgaris",
                    Descripcion = "Molusco con ocho brazos y alta inteligencia.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Carnívoro",
                    Tipo = "Molusco",
                    Tamano = "12 - 36 cm",
                    PesoPromedio = 3,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 52,
                    NombreComun = "Calamar",
                    NombreCientifico = "Teuthida",
                    Descripcion = "Molusco con cuerpo alargado y tentáculos.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Carnívoro",
                    Tipo = "Molusco",
                    Tamano = "20 - 60 cm",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 53,
                    NombreComun = "Almeja",
                    NombreCientifico = "Bivalvia",
                    Descripcion = "Molusco con dos valvas, filtrador.",
                    Habitat = "Aguas marinas",
                    Alimentacion = "Filtrador",
                    Tipo = "Molusco",
                    Tamano = "1 - 20 cm",
                    PesoPromedio = 0.2m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 54,
                    NombreComun = "Nautilus",
                    NombreCientifico = "Nautilus pompilius",
                    Descripcion = "Molusco con concha en espiral y tentáculos.",
                    Habitat = "Océano Indo-Pacífico",
                    Alimentacion = "Carnívoro",
                    Tipo = "Molusco",
                    Tamano = "15 - 25 cm",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Vulnerable",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 55,
                    NombreComun = "Sepia",
                    NombreCientifico = "Sepiida",
                    Descripcion = "Molusco con cuerpo ovalado y aletas.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Carnívoro",
                    Tipo = "Molusco",
                    Tamano = "15 - 25 cm",
                    PesoPromedio = 0.4m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 56,
                    NombreComun = "Mejillón",
                    NombreCientifico = "Mytilidae",
                    Descripcion = "Molusco con concha alargada, vive en grupos.",
                    Habitat = "Aguas marinas",
                    Alimentacion = "Filtrador",
                    Tipo = "Molusco",
                    Tamano = "5 - 10 cm",
                    PesoPromedio = 0.05m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 57,
                    NombreComun = "Calamar Gigante",
                    NombreCientifico = "Architeuthis dux",
                    Descripcion = "Molusco de gran tamaño, habitante de aguas profundas.",
                    Habitat = "Océanos profundos",
                    Alimentacion = "Carnívoro",
                    Tipo = "Molusco",
                    Tamano = "10 - 13 metros",
                    PesoPromedio = 275,
                    EstadoConservacion = "Datos insuficientes",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 58,
                    NombreComun = "Ostra",
                    NombreCientifico = "Ostreidae",
                    Descripcion = "Molusco con concha rugosa, filtrador.",
                    Habitat = "Aguas marinas",
                    Alimentacion = "Filtrador",
                    Tipo = "Molusco",
                    Tamano = "5 - 15 cm",
                    PesoPromedio = 0.1m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 59,
                    NombreComun = "Babosa Marina",
                    NombreCientifico = "Sacoglossa",
                    Descripcion = "Molusco marino sin concha, cuerpo blando.",
                    Habitat = "Océanos tropicales",
                    Alimentacion = "Herbívoro",
                    Tipo = "Molusco",
                    Tamano = "1 - 5 cm",
                    PesoPromedio = 0.01m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },
                new Animal
                {
                    AnimalId = 60,
                    NombreComun = "Caracol Marino",
                    NombreCientifico = "Littorina littorea",
                    Descripcion = "Molusco con concha en espiral, habitante de costas rocosas.",
                    Habitat = "Costas rocosas de océanos",
                    Alimentacion = "Herbívoro",
                    Tipo = "Molusco",
                    Tamano = "1 - 3 cm",
                    PesoPromedio = 0.005m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 6
                },

                new Animal
                {
                    AnimalId = 61,
                    NombreComun = "Cangrejo",
                    NombreCientifico = "Brachyura",
                    Descripcion = "Crustáceo con caparazón y pinzas.",
                    Habitat = "Océanos y costas",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "1 - 30 cm",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 62,
                    NombreComun = "Langosta",
                    NombreCientifico = "Palinuridae",
                    Descripcion = "Crustáceo con cuerpo alargado y antenas largas.",
                    Habitat = "Océanos",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "20 - 50 cm",
                    PesoPromedio = 1,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 63,
                    NombreComun = "Gamba",
                    NombreCientifico = "Caridea",
                    Descripcion = "Crustáceo pequeño, cuerpo alargado.",
                    Habitat = "Aguas marinas",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "2 - 10 cm",
                    PesoPromedio = 0.02m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 64,
                    NombreComun = "Langostino",
                    NombreCientifico = "Penaeidae",
                    Descripcion = "Crustáceo similar a la gamba, de mayor tamaño.",
                    Habitat = "Aguas marinas",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "10 - 20 cm",
                    PesoPromedio = 0.1m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 65,
                    NombreComun = "Centollo",
                    NombreCientifico = "Maja squinado",
                    Descripcion = "Crustáceo con caparazón espinoso.",
                    Habitat = "Océano Atlántico",
                    Alimentacion = "Carnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "10 - 20 cm",
                    PesoPromedio = 0.3m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 66,
                    NombreComun = "Bogavante",
                    NombreCientifico = "Homarus americanus",
                    Descripcion = "Crustáceo con pinzas grandes.",
                    Habitat = "Océano Atlántico",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "20 - 60 cm",
                    PesoPromedio = 1.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 67,
                    NombreComun = "Cangrejo Ermitaño",
                    NombreCientifico = "Paguroidea",
                    Descripcion = "Crustáceo que usa conchas vacías para protegerse.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "2 - 15 cm",
                    PesoPromedio = 0.1m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 68,
                    NombreComun = "Krill",
                    NombreCientifico = "Euphausiacea",
                    Descripcion = "Crustáceo pequeño, base de la cadena alimentaria marina.",
                    Habitat = "Océanos de todo el mundo",
                    Alimentacion = "Filtrador",
                    Tipo = "Crustáceo",
                    Tamano = "1 - 2 cm",
                    PesoPromedio = 0.001m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 69,
                    NombreComun = "Cigala",
                    NombreCientifico = "Nephropidae",
                    Descripcion = "Crustáceo similar a la langosta, de menor tamaño.",
                    Habitat = "Océano Atlántico",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "10 - 25 cm",
                    PesoPromedio = 0.2m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                },
                new Animal
                {
                    AnimalId = 70,
                    NombreComun = "Cangrejo de las Nieves",
                    NombreCientifico = "Chionoecetes opilio",
                    Descripcion = "Crustáceo adaptado a aguas frías.",
                    Habitat = "Océano Pacífico norte",
                    Alimentacion = "Omnívoro",
                    Tipo = "Crustáceo",
                    Tamano = "10 - 20 cm",
                    PesoPromedio = 0.5m,
                    EstadoConservacion = "Preocupación menor",
                    CategoriaId = 7
                }
            };
            
            await _database.InsertAllAsync(animalesIniciales);
        }

    }
}
