using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace apiWebUJED
{
    public class UpdateData
    {
        string constring = "DATA SOURCE=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.10)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ujeddev.ujed.mx))) ; PASSWORD = ujed2017; USER ID = portal_dev; ";
        public string RegistrarSalida(string FunctionData, string matricula)
        {
            string response = "0";
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand ComandoOracle = conn.CreateCommand())
                {
                    ComandoOracle.CommandText = "DECLARE V_SALIDA VARCHAR2(50); BEGIN PROC_SALIDA(:matricula, V_SALIDA); END;";
                    ComandoOracle.Parameters.Add("matricula", matricula);
                    ComandoOracle.ExecuteNonQuery();
                    response = "1"; 
                }
                conn.Close();
            }
            return response;
        }

        /*Update Estado encuesta*/
        public string EstadoEncuesta(string FunctionData, string id_encuesta, string estado)
        {
            string response = "0";
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand ComandoOracle = conn.CreateCommand())
                {
                    ComandoOracle.CommandText = "DECLARE V_SALIDA VARCHAR2(50); BEGIN PROC_UPDATE_ENCUESTA(:id_encuesta, :estado, V_SALIDA); END;";
                    ComandoOracle.Parameters.Add("id_encuesta", id_encuesta);
                    ComandoOracle.Parameters.Add("estado", estado);
                    ComandoOracle.ExecuteNonQuery();
                    response = "1";
                }
                conn.Close();
            }
            return response;
        }
    }   
}