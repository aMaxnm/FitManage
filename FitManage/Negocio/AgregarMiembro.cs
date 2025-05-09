using System;
using AccesoDatos;
using Entidades;

namespace Negocio
{
    public class AgregarMiembro
    {
        private MiembroDAO miembroDAO;

        public AgregarMiembro()
        {
            miembroDAO = new MiembroDAO();
        }

        //Falta hacer una validacion para la id
        public bool RegistrarMiembro(
            int idMembresia,
            string nombres,
            string apellidoPaterno,
            string apellidoMaterno,
            DateTime fechaNacimiento,
            string numeroTelefono,
            DateTime fechaInicio,
            DateTime fechaFin,
            byte[] fotografia)
        {
            
            if (!ValidarDatos.ValidarTexto(nombres) ||
                !ValidarDatos.ValidarTexto(apellidoPaterno) ||
                !ValidarDatos.ValidarTexto(apellidoMaterno) ||
                !ValidarDatos.ValidarTexto(numeroTelefono) ||
                !ValidarDatos.ValidarTexto(fechaNacimiento))
            {
                Console.WriteLine("Alguno de los espacios esta en blanco");
                return false;
            }

            if (!ValidarDatos.ValidarTelefono(numeroTelefono))
            {
                Console.WriteLine("Teléfono inválido.");
                return false;
            }

            if (fechaNacimiento >= DateTime.Now.Date)
            {
                Console.WriteLine("La fecha de nacimiento no puede ser hoy ni futura.");
                return false;
            }

            if (!ValidarDatos.ValidarFecha(fechaInicio) || !ValidarDatos.ValidarFecha(fechaFin))
            {
                Console.WriteLine("Fechas de inicio o fin inválidas.");
                return false;
            }

            if (fechaFin <= fechaInicio)
            {
                Console.WriteLine("La fecha de fin debe ser posterior a la fecha de inicio.");
                return false;
            }

            if (fotografia == null || fotografia.Length == 0)
            {
                Console.WriteLine("Debe cargar una imagen válida.");
                return false;
            }

            //Crear objeto Miembro
            Miembro nuevoMiembro = new Miembro
            {
                IdMembresia = idMembresia,
                Nombres = nombres,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                FechaNacimiento = fechaNacimiento,
                NumeroTelefono = numeroTelefono,
                FechaRegistro = DateTime.Now, // se genera automáticamente
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                Fotografia = fotografia
            };

            //Guardar en la base de datos
            try
            {
                miembroDAO.AgregarMiembro(nuevoMiembro);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el miembro: " + ex.Message);
                return false;
            }
        }
    }
}