using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidad;
using AccesoDatos;

namespace Negocio
{
    public class MembresiaServicio
    {
        public void EditarMembresia(Membresia m)
        {
            new MembresiaDAO().EditarMembresia(m);
        }

        public List<Membresia> ObtenerMembresias()
        {
            return new MembresiaDAO().ObtenerMembresias();
        }
    }

}
