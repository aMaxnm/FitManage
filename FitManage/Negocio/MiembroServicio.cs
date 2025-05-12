using System;
using System.Collections.Generic;
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

            if (!ValidarDatos.ValidarTelefono(numeroTelefono))
            {
                Console.WriteLine("Teléfono inválido.");
                return -1;
            }

            if (fechaNacimiento >= DateTime.Now.Date)
            {
                Console.WriteLine("La fecha de nacimiento no puede ser hoy ni futura.");
                return -1;
            }

            if (fotografia == null || fotografia.Length == 0)
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
                Console.WriteLine("Miembro ya registrado");
                return -2; // 🔸 Código especial para duplicado
            }
            else
            {
                //Guardar en la base de datos
                try
                {
                    int idGenerado = miembroDAO.AgregarMiembro(nuevoMiembro);
                    Console.WriteLine("Registro exitoso.");
                    return idGenerado;
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
