﻿using Practica01_405606.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica01_405606.Services
{
    public interface IArticuloService
    {
        bool Crear(Articulo articulo);
        List<Articulo> ObtenerTodos();
    }
}
