using System;
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
        public void Guardar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

                productoDAO.Insertar(producto);
        }

        public void EliminarStock(List<Producto> carrito)
        {
            productoDAO.EliminarStock(carrito);
        }

        // Puedes agregar más métodos según sea necesario, por ejemplo:
        // BuscarPorNombre, ActualizarProducto, EliminarProducto, etc.
    }
}