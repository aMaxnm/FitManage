using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    //posiblemente esta roto
    public class Membresia
    {
        public string Tipo { get; set; }
        public decimal Precio { get; set; }

        public int Id { get; set; }

        public int Duracion { get; set; }

        public string DescripcionCompleta
        {
            get { return $"{Tipo_membresia} - ${Precio}"; }
        }
    }
}
