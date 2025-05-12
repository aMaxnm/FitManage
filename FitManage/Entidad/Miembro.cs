using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Miembro
    {
        public int IdMiembro { get; set; }
        public int IdMembresia { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string NumeroTelefono { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public byte[] Fotografia { get; set; }
    }
}
