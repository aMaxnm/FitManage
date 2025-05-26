using System.Collections.Generic;
using AccesoDatos;
using Entidad;

namespace Negocio
{
    public class ProductoServicio
    {
        private readonly ProductoDAO productoDAO;

        public ProductoServicio()
        {
            productoDAO = new ProductoDAO();
        }

        public List<Producto> ObtenerTodos()
        {
            return productoDAO.ObtenerProductos();
        }

        public void EditarProducto(Producto p)
        {
            new ProductoDAO().EditarProducto(p);
        }
        // Puedes agregar más métodos según sea necesario, por ejemplo:
        // BuscarPorNombre, ActualizarProducto, EliminarProducto, etc.
    }
}