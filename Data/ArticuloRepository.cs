using Practica01_405606.Data.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data
{
    public class ArticuloRepository : IArticuloRepository
    {
        public bool Insertar(Articulo articulo)
        {
            var parametros = new List<ParameterSQL>
            {
                new ParameterSQL("@NombreArticulo", articulo.Nombre),
                new ParameterSQL("@Precio", articulo.PrecioUnitario)
            };

            return DataHelper.GetInstance()
                             .ExecuteSPDML("sp_insertar_articulo", parametros) > 0;
        }

        public List<Articulo> ObtenerTodos()
        {
            var lista = new List<Articulo>();
            var table = DataHelper.GetInstance().ExecuteSPQuery("sp_obtener_articulos");

            foreach (System.Data.DataRow row in table.Rows)
            {
                lista.Add(new Articulo
                {
                    Codigo = int.Parse(row["id_articulo"].ToString()),
                    Nombre = row["nombre_articulo"].ToString(),
                    PrecioUnitario = decimal.Parse(row["pre_unitario"].ToString())
                });
            }

            return lista;
        }
    }
}
