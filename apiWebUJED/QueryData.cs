using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace apiWebUJED
{
    public class QueryData
    {
/**
 * Proporciona datos para iniciar sesion en aplicacion
 * **/
        string constring = "DATA SOURCE=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.10)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME = ujeddev.ujed.mx))) ; PASSWORD = ujed2017; USER ID = portal_dev; ";
        public IDictionary<string, object> GetExist(string matricula)
        {
            var details = new Dictionary<string, object>();
            int existe = 0;
            int entro;
            int primera;
            int existeUjed = 0;

            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT F_EXIST(:matricula) EXIST FROM DUAL";
                    cmd.Parameters.Add("matricula", matricula);

                    var result = cmd.ExecuteScalar();
                    if (result.Equals("TRUE"))
                    {
                        existe = 1;
                        existeUjed = 1;
                    }
                    conn.Close();
                }

                conn.Open();
                using (OracleCommand COMANDO = conn.CreateCommand())
                {
                    COMANDO.CommandText = "SELECT F_EXIST_EXTERNO(:matricula) EXIST FROM DUAL";
                    COMANDO.Parameters.Add("matricula", matricula);

                    var result = COMANDO.ExecuteScalar();
                    if (result.Equals("TRUE"))
                    {
                        existe = 1;
                    }
        
                    conn.Close();
                }

                conn.Open();
                using (OracleCommand ComandoOracle = conn.CreateCommand())
                {
                    ComandoOracle.CommandText = "SELECT F_EXIST_ENTRADA(:matricula) EXIST FROM DUAL";
                    ComandoOracle.Parameters.Add("matricula", matricula);
                  
                    var result2 = ComandoOracle.ExecuteScalar();
                    if (result2.Equals("TRUE"))
                    {
                        entro = 1;
                    } 
                    else 
                    {
                        entro = 0;
                    }
                    conn.Close(); 
                 }


                conn.Open();
                using (OracleCommand ComandoOracle = conn.CreateCommand())
                {
                    ComandoOracle.CommandText = "SELECT F_PRIMERA_V(:matricula) EXIST FROM DUAL";
                    ComandoOracle.Parameters.Add("matricula", matricula);

                    var result3 = ComandoOracle.ExecuteScalar();
                    if (result3.Equals("TRUE"))
                    {
                        primera = 1;
                    }
                    else
                    {
                        primera = 0;
                    }
                    conn.Close();
                }

                 if (existe == 1 && primera == 1 && existeUjed == 1)
                 {
                    details.Add("respuesta", "primera");
                 }
                 else if (existe == 1 && entro == 0)
                 {
                    details.Add("respuesta", "procesos");      
                 }
                 else if (existe == 1 && entro == 1)
                 {
                    details.Add("respuesta", "preguntas");
                 }
                 else
                 {
                    details.Add("respuesta", "No existe");
                 }
            }
            return details;  
       }
        
 /**
   * Proporciona datos sobre las encuestas que estan activas
   * **/
        public DataTable GetEncuestasActivas(String FunctionData)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + ")";

                    var details = new Dictionary<string, object>();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

 /**
   * Obtener respuesta de si por lo menos una encuesta esta activa
   * **/
        public int GetExistEncuestaActiva(string matricula)
        {
            int respuesta;
          
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT F_EXIST_ENCUESTA_ACTIVA() EXIST FROM DUAL";

                    var result = cmd.ExecuteScalar();
                    if (result.Equals("TRUE"))
                    {
                        respuesta = 1;
                    }
                    else
                    {
                        respuesta = 0;
                    }

                    cmd.CommandText = "SELECT F_CONTESTO_ENCUESTA(:matricula) EXIST FROM DUAL";
                    cmd.Parameters.Add("matricula", matricula);
                    var result2 = cmd.ExecuteScalar();
                    if (result2.Equals("TRUE"))
                    {
                        respuesta = 0;
                    }
            
                    conn.Close();
                }

            }
            return respuesta;
        }

        
/**
 * Proporciona informacion de funciones almacenadas en la base de datos
 * **/
        public DataTable ShowProcesos(string FunctionData)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + ")";

                    var details = new Dictionary<string, object>();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                          
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos.");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

/**
  * Tipos de Usuarios
  * **/
        public DataTable ShowTiposUsuario(string FunctionData)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + ")";

                    var details = new Dictionary<string, object>();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                          
                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos.");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

/**
  * Proporciona informacion sobre el registro de entrada
  * **/
        public IDictionary<string, object> GetRegitroEntrada(string FunctionData, string matricula)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + "(:cve_alumno))";
                    cmd.Parameters.Add("cve_alumno", matricula);

                    var details = new Dictionary<string, object>();

                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(rdr);

                        if (dt.Rows.Count != 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {
                                    details.Add(column.ColumnName, row[column]);
                                }
                            }
                        }
                        else
                        {
                            details.Add("error", "No se encontró registro en la base de datos.");
                        }

                        conn.Close();
                        return details;
                    }
                }
            }
        }



/**
 * Proporciona informacion sobre el usuarios existentes en las tablas 
 * **/
        public DataTable GetBuscarUsuarios(String FunctionData, string paterno)
        {
            string Paterno = paterno.ToUpper();
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + "(:paterno))";
                    cmd.Parameters.Add("paterno", Paterno);


                    var details = new Dictionary<string, object>();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

        public IDictionary<string, object> GetSesion(string FunctionData, string matricula)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + "(:matricula))";
                    cmd.Parameters.Add("matricula", matricula);
                    
                    var details = new Dictionary<string, object>();

                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(rdr);

                        if (dt.Rows.Count != 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                foreach (DataColumn column in dt.Columns)
                                {

                                    if (row[column] == row["FOTO"])
                                    {
                                        if (row.IsNull("FOTO"))
                                        {
                                            string vacio = image.empty();
                                            details.Add(column.ColumnName, vacio);
                                        }
                                        else
                                        {
                                            string url = image.resize((byte[])row["FOTO"]);
                                            details.Add(column.ColumnName, url);
                                        }
                                    }
                                    else
                                    {
                                        details.Add(column.ColumnName, row[column]);
                                    }

                                }
                            }
                        }
                        else
                        {
                            details.Add("error", "No se encontró registro en la base de datos.");
                        }

                        conn.Close();
                        return details;

                    }
                }
            }
        }

        /**
 * Proporciona informacion sobre el usuarios existentes en las tablas 
 * **/
        public DataTable GetReporteEncuestas(string fecha_desde, string fecha_hasta, string tipoEncuesta, string turno)
        {
          
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    if (tipoEncuesta == "satisfaccion")
                    {
                        funcion("F_REPORTE_SATISFACCION", turno, cmd, fecha_desde, fecha_hasta);

                    }
                    else if (tipoEncuesta == "satisfaccionTotal")
                    {
                        funcion("F_REPORTE_SATISF_TOTAL", turno, cmd, fecha_desde, fecha_hasta);
                    }
                    else {
                        cmd.CommandText = "SELECT * FROM TABLE(F_REPORTE_SALIDA (:fecha_desde, :fecha_hasta))";
                        cmd.Parameters.Add("fecha_desde", fecha_desde);
                        cmd.Parameters.Add("fecha_hasta", fecha_hasta);
                    }
                   
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }


        public OracleCommand funcion(string FunctionData, string turno, OracleCommand cmd, string fecha_desde, string fecha_hasta)
        {
            cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + "(:fecha_desde, :fecha_hasta, :h_entrada, :h_fin))";
            cmd.Parameters.Add("fecha_desde", fecha_desde);
            cmd.Parameters.Add("fecha_hasta", fecha_hasta);
            if (turno == "1")
            {
                cmd.Parameters.Add("h_entrada", "08:00");
                cmd.Parameters.Add("h_fin", "20:30");
            }
            else if (turno == "2")
            {
                cmd.Parameters.Add("h_entrada", "08:00");
                cmd.Parameters.Add("h_fin", "14:30");
            }
            else if (turno == "3")
            {
                cmd.Parameters.Add("h_entrada", "14:31");
                cmd.Parameters.Add("h_fin", "20:30");
            }
            return cmd;
        }
        /**
* Proporciona informacion sobre el usuarios existentes en las tablas 
* **/
        public DataTable GetComentarios( string fecha_desde, string fecha_hasta, string tipoEncuesta, string turno)
        {

            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    if (tipoEncuesta == "satisfaccion")
                    {
                       funcion("F_COMENTARIOS_SATISFACCION", turno, cmd, fecha_desde, fecha_hasta);
                       
                    }
                    else
                    {
                        cmd.CommandText = "SELECT * FROM TABLE( F_COMENTARIOS_SALIDA(:fecha_desde, :fecha_hasta))";
                        cmd.Parameters.Add("fecha_desde", fecha_desde);
                        cmd.Parameters.Add("fecha_hasta", fecha_hasta);
                    }
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

        /**
* Proporciona informacion sobre el usuarios existentes en las tablas 
* **/
        public DataTable GetFiltroProcesos( string fecha_desde, string fecha_hasta, string tipoFiltro)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    if (tipoFiltro == "procesos")
                    {
                        cmd.CommandText = "SELECT * FROM TABLE(F_FILTRO_PROCESOS (:fecha_desde, :fecha_hasta))";
                        cmd.Parameters.Add("fecha_desde", fecha_desde);
                        cmd.Parameters.Add("fecha_hasta", fecha_hasta);
                    }
                    else
                    {
                        cmd.CommandText = "SELECT * FROM TABLE( F_FILTRO_CARRERA(:fecha_desde, :fecha_hasta))";
                        cmd.Parameters.Add("fecha_desde", fecha_desde);
                        cmd.Parameters.Add("fecha_hasta", fecha_hasta);
                    }
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

        /**
  * Mostrar todas las encuestas
  * **/
        public DataTable ShowEncuestas()
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM BIBLIOTECA_ENCUESTAS";

                    var details = new Dictionary<string, object>();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontraron datos.");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }

/**
  * Proporciona informacion de funciones almacenadas en la base de datos
  * **/
        public DataTable ShowPreguntas(string FunctionData)
        {
            using (OracleConnection conn = new OracleConnection(constring))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TABLE(" + FunctionData + ")";

                    var details = new Dictionary<string, object>();
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();

                        if (rdr.HasRows)
                        {
                            dt.Load(rdr);
                        }
                        else
                        {
                            dt.Columns.Add("error", typeof(System.String));
                            dt.Rows.Add("No se encontro dato.");
                        }
                        conn.Close();
                        return dt;
                    }
                }
            }
        }
    }
}