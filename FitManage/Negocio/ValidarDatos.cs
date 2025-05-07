using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ValidarDatos
    {
            public static bool ValidarTexto(string texto)
            {
                return !string.IsNullOrWhiteSpace(texto) && System.Text.RegularExpressions.Regex.IsMatch(texto, "^[a-zA-ZáéíóúÁÉÍÓÚñÑ]+$");
            }

            public static bool ValidarTelefono(string telefono)
            {
                return !string.IsNullOrWhiteSpace(telefono) && System.Text.RegularExpressions.Regex.IsMatch(telefono, "^[0-9]+$");
            }

            public static bool ValidarFecha(DateTime fechaSeleccionada)
            {
                return fechaSeleccionada.Date >= DateTime.Now.Date;
            }

            public static bool ValidarImagen(object imagen)
            {
                return imagen != null;
            }
    }
}
