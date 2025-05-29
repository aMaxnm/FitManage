using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos;
using Entidad;

namespace Negocio
{
    public class VentaServicio
    {
        private VentaDAO ventaDAO = new VentaDAO();
        public int RegistrarVenta(Venta venta)
        {
            return ventaDAO.InsertarVenta(venta);
        }
        public List<Venta> ObtenerTodasLasVentas()
        {
            return ventaDAO.ObtenerVentasConDetalles();
        }
    }
}
