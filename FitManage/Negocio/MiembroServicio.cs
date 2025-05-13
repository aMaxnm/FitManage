using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public void Actualizar(Miembro miembro)
        {
            // Validaciones de negocio
            if (string.IsNullOrWhiteSpace(miembro.Nombres))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(miembro.NumeroTelefono))
                throw new ArgumentException("El número telefónico no puede estar vacío.");

            if (!Regex.IsMatch(miembro.NumeroTelefono, @"^\d{10}$"))
                throw new ArgumentException("El número telefónico debe tener 10 dígitos.");

            // Llama al DAO para hacer el update real
            miembroDAO.Actualizar(miembro);
        }

    }

}
