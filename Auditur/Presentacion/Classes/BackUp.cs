using Auditur.Presentacion.Classes;
using Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Auditur.Negocio
{
    public class BackUp
    {
        public bool Resguardar()
        {
            bool blnRespuesta = false;
            try
            {
                string strFecha = Validators.FechaParaSaveFile(DateTime.Now.ToString());
                SaveFileDialog sfdGuardarComo = new SaveFileDialog();
                sfdGuardarComo.InitialDirectory = AccesoDatos.DirBD;

                sfdGuardarComo.Title = "Guardar Back Up de base de datos como...";
                sfdGuardarComo.Filter = "Base de datos(.sdf)|*.sdf";
                sfdGuardarComo.FileName = "BaseDeDatos_" + strFecha + ".sdf";
                if (sfdGuardarComo.ShowDialog() == DialogResult.OK)
                {
                    string strBackupDestino = sfdGuardarComo.FileName;
                    string strBackupOrigen = AccesoDatos.DirBD;
                    System.IO.File.Copy(strBackupOrigen, strBackupDestino, sfdGuardarComo.OverwritePrompt);
                    blnRespuesta = true;
                }
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return blnRespuesta;
        }

        public bool Cargar()
        {
            bool blnRespuesta = false;
            try
            {
                OpenFileDialog fdCargar = new OpenFileDialog();
                fdCargar.InitialDirectory = AccesoDatos.DirBACKUP;
                fdCargar.Title = "Cargar base de datos...";
                fdCargar.Filter = "Base de datos(.sdf)|*.sdf";
                if (fdCargar.ShowDialog() == DialogResult.OK)
                {
                    string strBackupOrigen = fdCargar.FileName;
                    string strBackupDestino = AccesoDatos.DirBD;

                    File.Copy(strBackupOrigen, strBackupDestino, true);
                    blnRespuesta = true;
                }
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return blnRespuesta;
        }
    }
}