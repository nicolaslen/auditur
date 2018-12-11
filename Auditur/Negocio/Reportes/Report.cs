using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public interface IReport<T>
        where T : class, new()
    {
        List<T> Generar(Semana oSemana);
    }
}
