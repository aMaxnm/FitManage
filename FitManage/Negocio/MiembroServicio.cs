using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidad;
namespace Negocio
{
    public class MiembroServicio
    {
       private MiembroDAO miembroDAO = new MiembroDAO();

        public List<Miembro> ObtenerTodos()
        {
            return miembroDAO.ObtenerMiembros();
        }
        public Miembro ObtenerMiembroPorId(int idMembresia)
        {
            return miembroDAO.ObtenerMiembroPorId(idMembresia);
        }
    }
}
