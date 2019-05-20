using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Helpers
{
    public static class Validators
    {
        #region IsNumeric
        public static bool IsNumeric(string strNumero)
        {
            return IsNumeric(strNumero, -1, int.MaxValue);
        }

        public static bool IsNumeric(string strNumero, int intValorMinimo)
        {
            return IsNumeric(strNumero, intValorMinimo, int.MaxValue);
        }

        public static bool IsNumeric(string strNumero, int intValorMinimo, int intValorMaximo)
        {
            return EsNumero(strNumero) && (Convert.ToInt32(strNumero) >= intValorMinimo) && (Convert.ToInt32(strNumero) <= intValorMaximo);
        }
        #endregion

        #region IsLong
        public static bool IsLong(string strNumero)
        {
            return IsLong(strNumero, -1, long.MaxValue);
        }

        public static bool IsLong(string strNumero, long longValorMinimo)
        {
            return IsLong(strNumero, longValorMinimo, long.MaxValue);
        }

        public static bool IsLong(string strNumero, long longValorMinimo, long longValorMaximo)
        {
            return EsLong(strNumero) && (Convert.ToInt64(strNumero) >= longValorMinimo) && (Convert.ToInt64(strNumero) <= longValorMaximo);
        }
        #endregion

        #region IsDouble
        public static bool IsDouble(string strDoble)
        {
            return IsDouble(strDoble, -1, int.MaxValue);
        }

        public static bool IsDouble(string strDoble, int intValorMinimo)
        {
            return IsDouble(strDoble, intValorMinimo, int.MaxValue);
        }

        public static bool IsDouble(string strDoble, int intValorMinimo, int intValorMaximo)
        {
            return EsDoble(strDoble) && (Convert.ToDouble(strDoble) >= intValorMinimo) && (Convert.ToDouble(strDoble) <= intValorMaximo);
        }
        #endregion

        #region IsStringPresent
        public static bool IsStringPresent(string strTexto)
        {
            return IsStringPresent(strTexto, 1, int.MaxValue);
        }
        public static bool IsStringPresent(string strTexto, int intLongMinima)
        {
            return IsStringPresent(strTexto, intLongMinima, int.MaxValue);
        }
        public static bool IsStringPresent(string strTexto, int intLongMinima, int intLongMaxima)
        {
            return (strTexto.Length >= intLongMinima) && (strTexto.Length <= intLongMaxima);
        }
        #endregion

        public static bool IsDateTime(string strTexto)
        {
            bool Devolver = false;

            try
            {
                DateTime Numero = Convert.ToDateTime(strTexto);
                Devolver = true;
            }
            catch
            {
            }

            return Devolver;
        }

        #region validateCUIT
        public static bool validateCUIT(string Cuit)
        {
            Regex rg = new Regex("[A-Z_a-z]");
            Cuit = Cuit.Replace("-", "");
            if (rg.IsMatch(Cuit))
                return false;
            if (Cuit.Length != 11)
                return false;
            char[] cuitArray = Cuit.ToCharArray();
            double sum = 0;
            int bint = 0;
            int j = 7;
            for (int i = 5, c = 0; c != 10; i--, c++)
            {
                if (i >= 2)
                    sum += (Char.GetNumericValue(cuitArray[c]) * i);
                else
                    bint = 1;
                if (bint == 1 && j >= 2)
                {
                    sum += (Char.GetNumericValue(cuitArray[c]) * j);
                    j--;
                }
            }
            if ((cuitArray.Length - (sum % 11)) == Char.GetNumericValue(cuitArray[cuitArray.Length - 1]))
                return true;
            return false;
        }
        #endregion

        private static bool EsNumero(string Cadena)
        {
            int output;
            return int.TryParse(Cadena, out output);
        }

        private static bool EsLong(string Cadena)
        {
            long output;
            return long.TryParse(Cadena, out output);
        }

        private static bool EsDoble(string Cadena)
        {
            double output;
            return double.TryParse(Cadena, out output);
        }

        #region isBisiesto
        public static bool isBisiesto(int Ano)
        {
            return ((Ano % 4 == 0) && (Ano % 100 != 0)) || (Ano % 400 == 0);
        }
        #endregion

        public static string FechaParaSaveFile(string strFecha)
        {
            strFecha = strFecha.Replace("/", "-");
            strFecha = strFecha.Replace(":", ".");
            return strFecha;
        }

        public static string Meses(int Mes)
        {
            switch (Mes)
            {
                case 1:
                    return "Enero";
                case 2:
                    return "Febrero";
                case 3:
                    return "Marzo";
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";
                case 6:
                    return "Junio";
                case 7:
                    return "Julio";
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "";
            }
        }

        public static string ConcatNumbers(string initValue, List<string> nextValues)
        {
            if (!string.IsNullOrWhiteSpace(initValue) && nextValues.Any())
            {
                var currentValue = initValue;
                foreach (var value in nextValues)
                {
                    int i = 1;
                    while (currentValue[currentValue.Length - i] == '9')
                    {
                        i++;
                    }

                    initValue += "/" + value.Substring(value.Length - i, i);
                    currentValue = value;
                }
            }

            return initValue;
        }
    }
}