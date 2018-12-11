using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;

namespace Helpers
{
    public static class AccesoDatos
    {
        public static string ConnectionString
        {
            get
            {
                return "Data Source=" + DirBD + ";File Mode=Read Write;Encrypt Database=True;Password=" + PasswordDB + ";Persist Security Info=False;";
            }
        }

        public const string PasswordDB = "david123";

        public static string DirBD
        {
            get
            {
                return Path + @"\Recursos\Auditur.sdf";
            }
        }

        public static string DirBACKUP
        {
            get
            {
                return Path + @"\Recursos\BackUp\";
            }
        }

        public static string Path
        {
            get
            {
                return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        public static SqlCeConnection OpenConn()
        {
            SqlCeConnection conn = new SqlCeConnection(AccesoDatos.ConnectionString);
            conn.Open();
            return conn;
        }

        #region ExecuteReader
        public static SqlCeDataReader ExecuteReader(string strComando, SqlCeConnection cnnConexion)
        {
            SqlCeDataReader rdrLector;
            CommandType cmdTipo;
            List<SqlCeParameter> parameterList = new List<SqlCeParameter>();
            cmdTipo = CommandType.StoredProcedure;

            rdrLector = ExecuteReader(strComando, cmdTipo, parameterList, cnnConexion);
            return rdrLector;
        }

        public static SqlCeDataReader ExecuteReader(string strComando, CommandType Tipo, SqlCeConnection cnnConexion)
        {
            SqlCeDataReader rdrLector;
            List<SqlCeParameter> parameterList = new List<SqlCeParameter>();


            rdrLector = ExecuteReader(strComando, Tipo, parameterList, cnnConexion);
            return rdrLector;
        }

        public static SqlCeDataReader ExecuteReader(string strComando, List<SqlCeParameter> parametersList, SqlCeConnection cnnConexion)
        {
            CommandType cmdTipo;
            SqlCeDataReader rdrLector;

            cmdTipo = CommandType.StoredProcedure;
            rdrLector = ExecuteReader(strComando, cmdTipo, parametersList, cnnConexion);
            return rdrLector;
        }

        public static SqlCeDataReader ExecuteReader(string strComando, CommandType Tipo, List<SqlCeParameter> parametersList, SqlCeConnection cnnConexion)
        {
            SqlCeDataReader rdrLector = null;
            try
            {
                SqlCeCommand cmdComando;

                cmdComando = new SqlCeCommand(strComando, cnnConexion);
                cmdComando.CommandType = Tipo;

                foreach (SqlCeParameter parameter in parametersList)
                    cmdComando.Parameters.AddWithValue(parameter.ParameterName, parameter.Value ?? DBNull.Value);
                
                System.Diagnostics.Debug.WriteLine(strComando);

                rdrLector = cmdComando.ExecuteReader();
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return rdrLector;
        }
        #endregion

        #region ExecuteNonQuery
        public static int ExecuteNonQuery(string strComando, SqlCeConnection cnnConexion)
        {
            CommandType cmdTipo;
            cmdTipo = CommandType.Text;
            List<SqlCeParameter> parameterList = new List<SqlCeParameter>();

            return ExecuteNonQuery(strComando, cmdTipo, parameterList, cnnConexion);
        }

        public static int ExecuteNonQuery(string strComando, CommandType Tipo, SqlCeConnection cnnConexion)
        {
            List<SqlCeParameter> parameterList = new List<SqlCeParameter>();

            return ExecuteNonQuery(strComando, Tipo, parameterList, cnnConexion);
        }

        public static int ExecuteNonQuery(string strComando, List<SqlCeParameter> parametersList, SqlCeConnection cnnConexion)
        {
            CommandType cmdTipo;
            cmdTipo = CommandType.StoredProcedure;
            return ExecuteNonQuery(strComando, cmdTipo, parametersList, cnnConexion);
        }

        public static int ExecuteNonQuery(string strComando, CommandType Tipo, List<SqlCeParameter> parametersList, SqlCeConnection cnnConexion)
        {
            int intRegsAfectados = 0;
            try
            {
                SqlCeCommand cmdComando;

                cmdComando = new SqlCeCommand(strComando, cnnConexion);
                cmdComando.CommandType = Tipo;

                foreach (SqlCeParameter parameter in parametersList)
                    cmdComando.Parameters.AddWithValue(parameter.ParameterName, parameter.Value ?? DBNull.Value);

                cmdComando.Prepare();

                intRegsAfectados = cmdComando.ExecuteNonQuery();
                System.Diagnostics.Debug.WriteLine(strComando);

                cmdComando.Dispose();
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return intRegsAfectados;
        }
        #endregion ExecuteNonQuery

        #region ExecuteDataSet
        public static DataSet ExecuteDataSet(string strComando, CommandType Tipo, string strTabla, SqlCeConnection cnnConexion, SqlCeDataAdapter daAdapter)
        {
            List<SqlCeParameter> parametersList = new List<SqlCeParameter>();
            DataSet ds = new DataSet();
            return ExecuteDataSet(ds, strComando, Tipo, strTabla, parametersList, cnnConexion, daAdapter);
        }

        public static DataSet ExecuteDataSet(DataSet ds, string strComando, CommandType Tipo, string strTabla, SqlCeConnection cnnConexion, SqlCeDataAdapter daAdapter)
        {

            List<SqlCeParameter> parametersList = new List<SqlCeParameter>();
            return ExecuteDataSet(ds, strComando, Tipo, strTabla, parametersList, cnnConexion, daAdapter);
        }

        public static DataSet ExecuteDataSet(string strComando, CommandType Tipo, string strTabla, List<SqlCeParameter> parametersList, SqlCeConnection cnnConexion, SqlCeDataAdapter daAdapter)
        {
            DataSet ds = new DataSet();
            return ExecuteDataSet(ds, strComando, Tipo, strTabla, parametersList, cnnConexion, daAdapter);
        }

        public static DataSet ExecuteDataSet(DataSet ds, string strComando, CommandType Tipo, string strTabla, List<SqlCeParameter> parametersList, SqlCeConnection cnnConexion, SqlCeDataAdapter daAdapter)
        {
            try
            {
                SqlCeCommand cmdComando;

                cmdComando = new SqlCeCommand(strComando, cnnConexion);
                cmdComando.CommandType = Tipo;

                foreach (SqlCeParameter parameter in parametersList)
                    cmdComando.Parameters.AddWithValue(parameter.ParameterName, parameter.Value ?? DBNull.Value);

                daAdapter = new SqlCeDataAdapter(cmdComando);
                daAdapter.Fill(ds, strTabla);
            }
            catch (Exception ex)
            {
                TextToFile.Errores(TextToFile.Error(ex));
            }
            return ds;
        }
        #endregion

    }
}