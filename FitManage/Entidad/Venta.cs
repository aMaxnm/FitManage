using System;
using System.Collections.Generic;

namespace Entidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoVenta { get; set; }
        public int IdMembresia { get; set; }

        // Agregar lista de detalles
        public List<DetalleVenta> Detalles { get; set; }

        // Constructor para inicializar la lista de detalles
        public Venta()
        {
            Detalles = new List<DetalleVenta>();
        }
    }
}