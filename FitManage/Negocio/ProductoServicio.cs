using AccesoDatos;
using Entidad;
using System.Collections.Generic;
using System;

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

    public void EditarProducto(Producto p)
    {
        productoDAO.EditarProducto(p);
    }

    public void EliminarStock(List<Producto> carrito)
    {
        productoDAO.EliminarStock(carrito);
    }

    public void Actualizar(Producto producto) 
    {
        if (producto == null)
            throw new ArgumentNullException(nameof(producto));

        productoDAO.EditarProducto(producto);
    }
}
