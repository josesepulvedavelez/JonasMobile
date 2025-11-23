using JonasMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonasMobile.Services
{
    public interface IAppService
    {
        Task<List<Categoria>> GetAllCategoriasAsync();
    }
}
