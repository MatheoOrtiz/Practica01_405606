using Practica01_405606.Data;
using Practica01_405606.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IArticuloRepository artRepo = new ArticuloRepository();
            IArticuloService artSvc = new ArticuloService(artRepo);


            artSvc.Crear(new Articulo { Nombre = "Lapicera", PrecioUnitario = 100m });
            artSvc.Crear(new Articulo { Nombre = "Cuaderno", PrecioUnitario = 500m });

            Console.WriteLine("Artículos en BD:");
            foreach (var a in artSvc.ObtenerTodos())
                Console.WriteLine($"[{a.Codigo}] {a.Nombre} - {a.PrecioUnitario}");


            IFacturaRepository facRepo = new FacturaRepository();
            IFacturaService facSvc = new FacturaService(facRepo);


            var factura = new Factura
            {
                Fecha = DateTime.Now,
                IdCliente = 1,
                IdFormaPago = 1
            };


            var lapicera = new Articulo { Codigo = 1, Nombre = "Lapicera", PrecioUnitario = 100m };
            var cuaderno = new Articulo { Codigo = 2, Nombre = "Cuaderno", PrecioUnitario = 500m };

            factura.AgregarDetalle(lapicera, 2);
            factura.AgregarDetalle(cuaderno, 1);

            int nro = facSvc.CrearFactura(factura);
            Console.WriteLine($"\nFactura creada N° {nro} - Total: {factura.Total()}");

            Console.WriteLine("\nFacturas con detalles:");
            foreach (var f in facSvc.ObtenerFacturas())
            {
                Console.WriteLine($"Factura {f.NroFactura} - Fecha {f.Fecha:d} - Total {f.Total()}");
                foreach (var d in f.Detalles)
                    Console.WriteLine($"   {d.Articulo.Nombre} x{d.Cantidad} = {d.Subtotal()}");
            }

            Console.WriteLine("\nOK.");
        }

    }
}
