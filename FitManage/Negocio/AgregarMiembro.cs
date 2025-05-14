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
        public int RegistrarMiembro(
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
                return -1;
            }

            if (!ValidarDatos.ValidarSoloNumeros(numeroTelefono))
            {
                Console.WriteLine("Teléfono inválido.");
                return -1;
            }

            if (fechaNacimiento >= DateTime.Now.Date)
            {
                Console.WriteLine("La fecha de nacimiento no puede ser hoy ni futura.");
                return -1;
            }

            if(fotografia == null || fotografia.Length == 0)
            {
                Console.WriteLine("Debe cargar una imagen válida.");
                return -1;
            }

            //Crear objeto Miembro
            MembresiaDAO membresiaSeleccionada = new MembresiaDAO();
            Membresia mem = membresiaSeleccionada.ObtenerMembresiaPorId(idMembresia); // Método para obtener la membresía
            if (mem == null)
            {
                Console.WriteLine("Error: No se encontró la membresía con el ID: " + idMembresia);
                return -1;
            }

            Miembro nuevoMiembro = new Miembro()
            {
                IdMiembro = numero,
                IdMembresia = idMembresia,
                Nombres = nombres,
                ApellidoPaterno = apellidoPaterno,
                ApellidoMaterno = apellidoMaterno,
                FechaNacimiento = fechaNacimiento,
                NumeroTelefono = numeroTelefono,
                FechaRegistro = DateTime.Now, // Se genera automáticamente
                FechaVencimiento = FechaRegistro.AddDays(mem.Duracion), //Obtiene la duración de la membresia y la utiliza para generar la fecha de vencimiento
                Fotografia = fotografia
            };
            if (miembroDAO.MiembroExiste(nombres, apellidoPaterno, apellidoMaterno, numeroTelefono))
            {
                Console.WriteLine("Miembro registrado");
                return -1; // ?? No lo registra si ya existe
            }
            else
            {

                //Guardar en la base de datos
                try
                {
                    miembroDAO.AgregarMiembro(nuevoMiembro);
                    Console.WriteLine("Registro exitoso.");
                    return numero;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al registrar el miembro: " + ex.Message);
                    Console.WriteLine("Detalles: " + ex.StackTrace); // Para ver dónde ocurre el error
                    return -1;
                }
            }
        }
    }
}