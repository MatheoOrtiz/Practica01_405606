using Practica01_405606.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Services
{
    public class ArticuloService : IArticuloService
    {
        private readonly IArticuloRepository _repo;

        public ArticuloService(IArticuloRepository repo)
        {
            _repo = repo;
        }

        public bool Crear(Articulo articulo) => _repo.Insertar(articulo);

        public List<Articulo> ObtenerTodos() => _repo.ObtenerTodos();
    }
}
