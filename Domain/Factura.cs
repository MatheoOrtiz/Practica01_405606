using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data
{
    public class Factura
    {
        public int NroFactura { get; set; }
        public DateTime Fecha { get; set; }
        public int IdCliente { get; set; }      
        public int IdFormaPago { get; set; }

        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();

        public decimal Total() => Detalles.Sum(d => d.Subtotal());

        public void AgregarDetalle(Articulo articulo, int cantidad)
        {
            var existente = Detalles.FirstOrDefault(d => d.Articulo.Codigo == articulo.Codigo);
            if (existente != null)
                existente.Cantidad += cantidad;
            else
                Detalles.Add(new DetalleFactura { Articulo = articulo, Cantidad = cantidad });
        }
    }
}
