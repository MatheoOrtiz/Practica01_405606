using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Data
{
    public interface IArticuloRepository
    {
        bool Insertar(Articulo articulo);
        List<Articulo> ObtenerTodos();
    }
}
