using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;

namespace Auditur.Negocio.Reportes
{
    public static class Extensions
    {
        public static string GetStringOrNull(this SqlCeDataReader rdrLector, int ordinal)
        {
            return !rdrLector.IsDBNull(ordinal)
                ? rdrLector.GetString(ordinal)
                : null;
        }

        public static DateTime? GetDateTimeOrNull(this SqlCeDataReader rdrLector, int ordinal)
        {
            return !rdrLector.IsDBNull(ordinal)
                ? (DateTime?)rdrLector.GetDateTime(ordinal)
                : null;
        }
    }
}
