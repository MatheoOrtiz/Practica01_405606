using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data
{
    public class DetalleFactura
    {
        public int Codigo { get; set; }         
        public Articulo Articulo { get; set; }   
        public int Cantidad { get; set; }

        public decimal Subtotal()
        {
            return Cantidad * (Articulo?.PrecioUnitario ?? 0m);
        }
    }
}
