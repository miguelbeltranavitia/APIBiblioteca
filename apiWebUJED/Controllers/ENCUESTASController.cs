using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace apiWebUJED.Controllers
{
    public class ENCUESTASController : ApiController
    {
        public DataTable Get()
        {
            QueryData consulta = new QueryData();
            var query = consulta.ShowEncuestas();

            return query;
        }

        public Object Put(string id_encuesta, string estado)
        {
            string FunctionData = "PROC_UPDATE_ENCUESTA";
            UpdateData update = new UpdateData();
            var query = update.EstadoEncuesta(FunctionData, id_encuesta, estado);
            return query;

        }
    }
}
