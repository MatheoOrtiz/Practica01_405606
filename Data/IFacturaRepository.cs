using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data
{
    public interface IFacturaRepository
    {
        int InsertarFactura(Factura factura);                   
        bool InsertarDetalle(int nroFactura, DetalleFactura d); 
        List<Factura> ObtenerFacturasConDetalles();             
    }
}
