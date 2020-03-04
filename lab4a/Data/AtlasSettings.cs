using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab4a.Data
{
    public class AtlasSettings : IAtlasSettings
    {
        public string Collection { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
    }
    public interface IAtlasSettings
    {
        string Collection { get; set; }
        string Database { get; set; }
        string ConnectionString { get; set; }
    }
}
