using System;
using AccesoDatos;
using Entidad;

namespace Negocio
{
    public class AgregarMiembro
    {
        private MiembroDAO miembroDAO;
        static Random rand = new Random();
        static int numero = rand.Next(1, 601) + 100; 
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
            DateTime FechaRegistro,
            byte[] fotografia)
        {
            
            if (!ValidarDatos.ValidarTexto(nombres) ||
                !ValidarDatos.ValidarTexto(apellidoPaterno) ||
                !ValidarDatos.ValidarTexto(apellidoMaterno))
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

            /*if (!ValidarDatos.ValidarFecha(fechaInicio) || !ValidarDatos.ValidarFecha(fechaFin))
            {
                Console.WriteLine("Fechas de inicio o fin inválidas.");
                return false;
            }

            if (fechaFin <= fechaInicio)
            {
                Console.WriteLine("La fecha de fin debe ser posterior a la fecha de inicio.");
                return false;
            }*/

            /*if (fotografia == null || fotografia.Length == 0)
            {
                Console.WriteLine("Debe cargar una imagen válida.");
                return false;
            }*/

            //Crear objeto Miembro
            MembresiaDAO embresiaSeleccionada = new MembresiaDAO();
            Membresia mem = embresiaSeleccionada.ObtenerMembresiaPorId(idMembresia); // Método para obtener la membresía
            if (mem == null)
            {
                Console.WriteLine("Error: No se encontró la membresía con el ID: " + idMembresia);
                return false;
            }

            Miembro nuevoMiembro = new Miembro()
            {
                IdMiembro = numero,
                IdMembresia = idMembresia,
                Nombre = nombres,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                FechaNacimiento = fechaNacimiento,
                NumeroCelular = numeroTelefono,
                FechaRegistro = DateTime.Now, // Se genera automáticamente
                FechaVencimiento = FechaRegistro.AddDays(mem.Duracion), // Accede correctamente a la duración
                Foto = fotografia
            };

            //Guardar en la base de datos
            try
            {
                miembroDAO.AgregarMiembro(nuevoMiembro);
                Console.WriteLine("Registro exitoso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al registrar el miembro: " + ex.Message);
                Console.WriteLine("Detalles: " + ex.StackTrace); // Para ver dónde ocurre el error
                return false;
            }
        }
    }
}