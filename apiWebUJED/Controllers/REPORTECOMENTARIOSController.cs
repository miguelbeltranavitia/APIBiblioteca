using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class REPORTECOMENTARIOSController : ApiController
    {
        public DataTable Get(string fecha_desde, string fecha_hasta, string tipoEncuesta, string turno)
        {
            QueryData consulta = new QueryData();
            var query = consulta.GetComentarios(fecha_desde, fecha_hasta, tipoEncuesta, turno);

            return query;
        }
    }
}
