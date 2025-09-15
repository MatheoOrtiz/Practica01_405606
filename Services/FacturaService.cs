using Practica01_405606.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _repo;

        public FacturaService(IFacturaRepository repo)
        {
            _repo = repo;
        }

        public int CrearFactura(Factura factura)
        {
            int nro = _repo.InsertarFactura(factura);
            foreach (var d in factura.Detalles)
                _repo.InsertarDetalle(nro, d);

            return nro;
        }

        public List<Factura> ObtenerFacturas() => _repo.ObtenerFacturasConDetalles();
    }
}
