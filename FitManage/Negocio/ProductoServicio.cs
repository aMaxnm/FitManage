using AccesoDatos;
using Entidad;
using System.Collections.Generic;
using System;
using System.Linq;

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

        ValidarProducto(producto);


        productoDAO.Insertar(producto);
    }


    public void EditarProducto(Producto p) {
        ValidarProducto(p);
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

        ValidarProducto(producto);
        productoDAO.EditarProducto(producto);
    }

    public void ValidarProducto(Producto producto)
    {
        if (producto == null)
            throw new ArgumentNullException(nameof(producto));

        // Validar nombre
        if (string.IsNullOrWhiteSpace(producto.Nombre))
            throw new ArgumentException("campo nombre vacío");

        if (producto.Nombre.Length > 200)
            throw new ArgumentException("máximo 200 caracteres");

        // Validar descripción
        if (string.IsNullOrWhiteSpace(producto.Descripcion))
            throw new ArgumentException("campo descripcion vacío");

        if (producto.Descripcion.Length > 200)
            throw new ArgumentException("máximo 200 caracteres");

        // Validar precio
        if (producto.Precio == 0)
            throw new ArgumentException("campo precio vacío");

        if (producto.Precio < 0 || producto.Precio > 100_000_000)
            throw new ArgumentException("debe estar entre 0 y 100,000,000");

        // Validar que precio no tenga caracteres inválidos
        if (!decimal.TryParse(producto.Precio.ToString(), out _))
            throw new ArgumentException("solo se aceptan números");

        // Validar cantidad
        if (producto.Cantidad == 0)
            throw new ArgumentException("no puede ser la cantidad 0");

        if (producto.Cantidad < 0 || producto.Cantidad > 100_000_000)
            throw new ArgumentException("debe estar entre 0 y 100,000,000");

        // Verificar que cantidad sea entero (esto es redundante porque es int, pero para generalizar)
        if (producto.Cantidad.ToString().Contains('.'))
            throw new ArgumentException("solo números enteros");

        // Validación adicional por si se recibe de una fuente externa como string
        if (!int.TryParse(producto.Cantidad.ToString(), out _))
            throw new ArgumentException("solo se aceptan números enteros");
    }

}
