using Practica01_405606.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data
{
    public class FacturaRepository : IFacturaRepository
    {
        public int InsertarFactura(Factura factura)
        {
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@Fecha", factura.Fecha),
                new ParameterSQL("@IdFormaPago", factura.IdFormaPago),
                new ParameterSQL("@IdCliente", factura.IdCliente)
            };


            var result = DataHelper.GetInstance().ExecuteScalar("sp_insertar_factura", parametros);
            return System.Convert.ToInt32(result);
        }

        public bool InsertarDetalle(int nroFactura, DetalleFactura d)
        {
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@NroFactura", nroFactura),
                new ParameterSQL("@IdArticulo", d.Articulo.Codigo),
                new ParameterSQL("@Cantidad", d.Cantidad)
            };

            return DataHelper.GetInstance().ExecuteSPDML("sp_insertar_detalle", parametros) > 0;
        }

        public List<Factura> ObtenerFacturasConDetalles()
        {
            var lista = new List<Factura>();
            var tabla = DataHelper.GetInstance().ExecuteSPQuery("sp_obtener_facturas_detalle");

            Factura facturaActual = null;
            int nroAnterior = -1;

            foreach (System.Data.DataRow row in tabla.Rows)
            {
                int nro = int.Parse(row["nro_factura"].ToString());
                if (nro != nroAnterior)
                {
                    facturaActual = new Factura
                    {
                        NroFactura = nro,
                        Fecha = System.DateTime.Parse(row["fecha"].ToString()),

                    };
                    lista.Add(facturaActual);
                    nroAnterior = nro;
                }

                var det = new DetalleFactura
                {
                    Articulo = new Articulo
                    {
                        Codigo = int.Parse(row["id_articulo"].ToString()),
                        Nombre = row["nombre_articulo"].ToString(),
                        PrecioUnitario = decimal.Parse(row["pre_unitario"].ToString())
                    },
                    Cantidad = int.Parse(row["cantidad"].ToString())
                };

                facturaActual.Detalles.Add(det);
            }

            return lista;
        }
    }
}
