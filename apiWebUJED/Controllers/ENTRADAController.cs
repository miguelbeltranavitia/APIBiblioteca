using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace apiWebUJED.Controllers
{
    public class ENTRADAController : ApiController
    {
        public Object Post([FromBody]Object data)
        {
            
            string FunctionData = "PROC_ENTRADA";
            InsertData insert = new InsertData();
            var query = insert.InsertRegistro(FunctionData, data);
            return query;
        }

        public IDictionary<string, object> Get(string matricula)
        {
            string FunctionData = "F_REGISTRO_ENTRADA";
            QueryData consulta = new QueryData();
            var query = consulta.GetRegitroEntrada(FunctionData,matricula);

            return query;
        }
       
    }
}
