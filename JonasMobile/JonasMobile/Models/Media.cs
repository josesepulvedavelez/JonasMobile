using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonasMobile.Models
{
    public class Media
    {
        [PrimaryKey]
        public int MediaId { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public int AnimalId { get; set; }
    }
}
