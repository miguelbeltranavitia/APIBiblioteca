using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

namespace apiWebUJED
{
    public class InsertData
    {
        string constring = "DATA SOURCE=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.10)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ujeddev.ujed.mx))) ; PASSWORD = ujed2017; USER ID = portal_dev; ";
        public string InsertRegistro(string FunctionData, Object data)
        {
            string response;
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open(); 
 
                var json = data.ToString();
                string matricula = JObject.Parse(json)["matricula"].ToString();
                string proceso = JObject.Parse(json)["descripcion"].ToString().ToUpper();

                using (OracleCommand ComandoOracle = conn.CreateCommand())
                {
                    ComandoOracle.CommandText = "SELECT F_EXIST_ENTRADA(:matricula) EXIST FROM DUAL";
                    ComandoOracle.Parameters.Add("matricula", matricula);
                    var result = ComandoOracle.ExecuteScalar();
                    if (result.Equals("TRUE"))
                    {
                        response = "0";
                    }
                    else 
                    {
                        ComandoOracle.CommandText = "DECLARE V_SALIDA VARCHAR2(50); BEGIN PROC_ENTRADA(:matricula, :proceso, V_SALIDA); END;";
                        ComandoOracle.Parameters.Add("matricula", matricula);
                        ComandoOracle.Parameters.Add("proceso", proceso);
                        ComandoOracle.ExecuteNonQuery();
                        response = "1";
                    }
                }
                conn.Close();
            }
            return response;
        }


 /**
   * Insertar respuestas y encuesta para tipo de salida y/o de satisfaccion
   */
        public string InsertEncuestas(Object data)
        {
            string response = "";
            string encuestaContestadaId = "";
            string pregunta = "";
            string pregunta_id = "";  

            var json = data.ToString();
            string matricula = JObject.Parse(json)["matricula"].ToString();
            string encuestaId = JObject.Parse(json)["encuestaId"].ToString();
            string tipoUsuario = JObject.Parse(json)["tipoUsuario"].ToString();
            string registroBibliotecaId = JObject.Parse(json)["registroBibliotecaId"].ToString();
            string tipoEncuesta = JObject.Parse(json)["tipoEncuesta"].ToString();

            int encuesta_id = Int32.Parse(encuestaId);
            int registro_biblioteca_id = Int32.Parse(registroBibliotecaId);

            using (OracleConnection conn = new OracleConnection(constring))
            {

                using (OracleCommand ComandoOracle = new OracleCommand("PROC_ENCUESTA_CONTESTADA",conn))
                {
                    ComandoOracle.CommandType = CommandType.StoredProcedure;
                    ComandoOracle.Parameters.AddWithValue("V_MATRICULA", matricula);
                    ComandoOracle.Parameters.AddWithValue("V_TIPO_USUARIO", tipoUsuario);
                    ComandoOracle.Parameters.AddWithValue("V_ENCUESTA_ID", encuesta_id);
                    ComandoOracle.Parameters.AddWithValue("V_REGISTRO_BIBLIOTECA_ID", registro_biblioteca_id);
                    ComandoOracle.Parameters.Add("V_SALIDA", OracleType.Number);
                    ComandoOracle.Parameters["V_SALIDA"].Direction = ParameterDirection.Output;

                    conn.Open();
                    ComandoOracle.ExecuteNonQuery();
                    conn.Close();
                    encuestaContestadaId = ComandoOracle.Parameters["V_SALIDA"].Value.ToString();
                }

                int encuesta_Contestada_Id = Int32.Parse(encuestaContestadaId);
                int preguntaId;
                var tamaño = ((JObject.Parse(json).Count)-5) / 2;
                for (int i = 1; i <= tamaño; i++)
                {
                    if (tipoEncuesta.Equals("salida"))
                    {
                        pregunta = JObject.Parse(json)["pregunta" + i].ToString();
                        pregunta_id = JObject.Parse(json)["pregunta_id" + i].ToString();
                    }
                    else if (tipoEncuesta.Equals("satisfaccion"))
                    { 
                        pregunta = JObject.Parse(json)["2pregunta" + i].ToString();
                        pregunta_id = JObject.Parse(json)["2pregunta_id" + i].ToString();
                    }
                    
                    preguntaId = Int32.Parse(pregunta_id);

                    conn.Open(); 
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DECLARE V_SALIDA VARCHAR2(50); BEGIN PROC_RESPUESTAS(:pregunta, :encuestaContestadaId, :pregunta_id, V_SALIDA); END;";
                        cmd.Parameters.Add("pregunta", pregunta);
                        cmd.Parameters.Add("encuestaContestadaId", encuesta_Contestada_Id);
                        cmd.Parameters.Add("pregunta_id", preguntaId);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                 }
                 response = "1";
            }
            return response;
        }


        /**
   * Insertar nueva encuesta de satisfaccion
   */
        public string NuevaEncuesta(Object data)
        {
            string response = "";
            string encuestaId = "";
            var json = data.ToString();
            string encuestaNueva = JObject.Parse(json)["encuestaNueva"].ToString().ToUpper();

            using (OracleConnection conn = new OracleConnection(constring))
            {

                using (OracleCommand ComandoOracle = new OracleCommand("PROC_ENCUESTA", conn))
                {
                    ComandoOracle.CommandType = CommandType.StoredProcedure;
                    ComandoOracle.Parameters.AddWithValue("V_NOMBRE", encuestaNueva);
                    ComandoOracle.Parameters.Add("V_SALIDA", OracleType.Number);
                    ComandoOracle.Parameters["V_SALIDA"].Direction = ParameterDirection.Output;

                    conn.Open();
                    ComandoOracle.ExecuteNonQuery();
                    conn.Close();
                    encuestaId = ComandoOracle.Parameters["V_SALIDA"].Value.ToString();
                }

                int encuesta_Id = Int32.Parse(encuestaId);
                int tipo = 0;
                for (int i = 1, j = 8; i <= 6; i++, j++)
                {
                    if (i==6)
                    {
                        tipo = 1;
                    }
                    else
                    {
                        tipo = 5;
                    }
                        conn.Open();
                        using (OracleCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "DECLARE V_SALIDA VARCHAR2(50); BEGIN PROC_PREGUNTAS_ENCUESTAS(:encuesta_Id, :no_pregunta, :pregunta_id, :tipo, V_SALIDA); END;";
                            cmd.Parameters.Add("encuesta_Id", encuesta_Id);
                            cmd.Parameters.Add("no_pregunta", i);
                            cmd.Parameters.Add("pregunta_id", j);
                            cmd.Parameters.Add("tipo", tipo);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    
                }
                response = "1";
            }
            return response;
        }
 /**
 * Insertar respuestas y encuesta para tipo de salida y/o de satisfaccion
 */
        public string InsertUsuarioExterno(Object data)
        {
            string response = "";
            string matricula = "HDG1";

            var json = data.ToString();
            string nombre = JObject.Parse(json)["nombre"].ToString().ToUpper();
            string paterno = JObject.Parse(json)["paterno"].ToString().ToUpper();
            string materno = JObject.Parse(json)["materno"].ToString().ToUpper();
            string genero = JObject.Parse(json)["genero"].ToString().ToUpper();
            string correo = JObject.Parse(json)["correo"].ToString();
            string institucion = JObject.Parse(json)["institucion"].ToString().ToUpper();
            string carrera = JObject.Parse(json)["carrera"].ToString().ToUpper();

            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand ComandoOracle = conn.CreateCommand())
                {
                    ComandoOracle.CommandText = "SELECT F_EXIST_BIBLIOTECA_CORREO(:correo) EXIST FROM DUAL";
                    ComandoOracle.Parameters.Add("correo", correo);
                    var result = ComandoOracle.ExecuteScalar();
                    if (result.Equals("TRUE"))
                    {
                        response = "0";
                    }
                    else 
                    {
                        using (OracleCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "SELECT SEQ_BIBLIOTECA_MATRICULA.nextval FROM DUAL";
                            var result2 = cmd.ExecuteScalar();

                            string secuencia = result2.ToString();
                            int length = secuencia.Length;
                            if (length == 1)
                            {
                                matricula += "000" + secuencia;
                            }
                            else if (length == 2)
                            {
                                matricula += "00" + secuencia;
                            }
                            else if (length == 3)
                            {
                                matricula += "0" + secuencia;
                            }
                            else if (length == 4)
                            {
                                matricula += secuencia;
                            }
                        }
                        conn.Close();
                        using (OracleCommand CMDOracle = new OracleCommand("PROC_BIBLIOTECA_USUAR_EXTERNOS", conn))
                        {
                            CMDOracle.CommandType = CommandType.StoredProcedure;
                            CMDOracle.Parameters.AddWithValue("V_MATRICULA", matricula);
                            CMDOracle.Parameters.AddWithValue("V_NOMBRE", nombre);
                            CMDOracle.Parameters.AddWithValue("V_PATERNO", paterno);
                            CMDOracle.Parameters.AddWithValue("V_MATERNO", materno);
                            CMDOracle.Parameters.AddWithValue("V_GENERO", genero);
                            CMDOracle.Parameters.AddWithValue("V_CORREO", correo);
                            CMDOracle.Parameters.AddWithValue("V_INSTITUCION", institucion);
                            CMDOracle.Parameters.AddWithValue("V_CARRERA", carrera);
                            CMDOracle.Parameters.Add("V_SALIDA", OracleType.Number);
                            CMDOracle.Parameters["V_SALIDA"].Direction = ParameterDirection.Output;

                            conn.Open();
                            CMDOracle.ExecuteNonQuery();
                            conn.Close();
                            CMDOracle.Parameters["V_SALIDA"].Value.ToString();
                            return response = matricula;
                        }
                    }
                }
            }
            return response;
        }
    }
}