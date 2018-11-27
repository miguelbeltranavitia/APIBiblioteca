using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class EXISTController : ApiController
    {
        //GET api/values/00J5004
       /* public IDictionary<string, object> Get(string id)
        {
            string cve_usuario = Seguridad.EncriptacionDos.Codifica(id.ToUpper());
            string FunctionData = "F_SESION";

            QueryData consulta = new QueryData();
            var query = consulta.GetSession(FunctionData, cve_usuario);

            return query;
        }*/
        public IDictionary<string, object> Get(string id)
        {
      
            QueryData consulta = new QueryData();
            var query = consulta.GetExist(id);

            return query;
        }
    }
}
