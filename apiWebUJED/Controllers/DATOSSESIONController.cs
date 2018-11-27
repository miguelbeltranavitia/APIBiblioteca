using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class DATOSSESIONController : ApiController
    {
        public IDictionary<string, object> Get(string matricula)
        {
            string FunctionData = "F_DATOS_SESION";
            QueryData consulta = new QueryData();
            var query = consulta.GetSesion(FunctionData, matricula);

            return query;
        }
    }
}
