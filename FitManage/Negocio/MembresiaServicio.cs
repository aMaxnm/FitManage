using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidad;

namespace Negocio
{
    public class MembresiaServicio
    {
        private MembresiaDAO membresiaDAO = new MembresiaDAO();

        public List<Membresia> ObtenerMembresias()
        {
            return membresiaDAO.ObtenerMembresias();
        }
    }
}
