using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Helpers
{
    public class TextToFile
    {
        private static string strPathErrores()
        {
            return AccesoDatos.Path + @"\Recursos\Errors (" + Validators.FechaParaSaveFile(DateTime.Today.ToShortDateString()) + " - " + Validators.FechaParaSaveFile(DateTime.Now.ToShortTimeString()) + ").txt";
        }

        public static int Errores(String strError)
        {
            try
            {
                int intErrores = 1;
                if (File.Exists(strPathErrores()))
                {
                    File.Delete(strPathErrores());
                }
                using (StreamWriter sw = File.CreateText(strPathErrores()))
                {
                    sw.WriteLine(strError);
                    sw.Close();
                    return intErrores;
                }
            }
            catch
            {
                return -10;
            }
        }

        public static string Error(Exception e)
        {
            return (e.TargetSite != null ? e.TargetSite.Name + " - " : "") + e.Data + " - " + e.Source + " - " + e.Message;
        }
    }
}
